using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using handson.Models;
using System.Text.RegularExpressions;
using Microsoft.VisualBasic;

namespace handson.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContasController : ControllerBase
    {
        private readonly Context _context;

        public ContasController(Context context)
        {
            _context = context;
        }

        // GET: api/Contas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Contas>>> Getcontas()
        {
            return await _context.contas.ToListAsync();
        }

        // GET: api/Contas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Contas>> GetContas(string id)
        {
            var contas = await _context.contas.FindAsync(id);

            if (contas == null)
            {
                return NotFound();
            }

            return contas;
        }



        // PUT: api/Contas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContas(string id, Contas contas)
        {
            if (id != contas.id)
            {
                return BadRequest();
            }

            _context.Entry(contas).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContasExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Contas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Contas>> PostContas(Contas contas)
        {            
            if (contas.id.Contains('.')){
                var idPai = contas.id.Substring(0, contas.id.LastIndexOf('.'));
                var id = await _context.contas.Where(a => a.id.Equals(idPai)).Select(a => new { a.lancamento, a.tipo }).ToListAsync();
                if (id.Count == 0)
                {
                    return Content("A conta pai não existe");
                }
                else if (id[0].lancamento)
                {
                    return Content("A conta pai não aceita contas filhas");
                }
                else
                {
                    contas.tipo = id[0].tipo;
                }
            }                              

            _context.contas.Add(contas);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ContasExists(contas.id))
                {
                    return Conflict("Já existe o código cadastrado");
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetContas", new { id = contas.id }, contas);
        }

        // DELETE: api/Contas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContas(string id)
        {
            var contas = await _context.contas.FindAsync(id);
            if (contas == null)
            {
                return NotFound();
            }

            _context.contas.Remove(contas);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // GET: api/Contas/5
        [HttpGet("{id}/GetSuggestionId")]
        public async Task<ActionResult<String>> GetSuggestionId(string id)
        {
            var v_valueReturn = id;

            while (true) {

                var ids = await _context.contas.Where(a => a.id.StartsWith(id)).Select(a =>  a.id ).ToListAsync();

                if (ids.Count() == 0){
                    return "A conta pai selecionada não existe";
                }
                Regex rx = new Regex("^" + id.Replace(".", "\\.") + "\\.[0-9]$|^" + id.Replace(".", "\\.") + "\\.[0-9][0-9]$|^" + id.Replace(".", "\\.") + "\\.[0-9][0-9][0-9]$");
                var valuesMatch = new List<int> { };
                
                    foreach( String value in ids)
                    {
                    if (rx.IsMatch(value))
                        {
                            valuesMatch.Add(int.Parse(value.Replace(id + ".", "")));
                        }
                    }
                if (valuesMatch.Count > 0)
                {
                    if (valuesMatch.BinarySearch(999) < 0)
                    {
                        int soma = valuesMatch.Max() + 1;
                        return id + "." + soma;
                    }
                    else
                    {
                        id = id.Substring(0, id.LastIndexOf("."));
                    }
                }
                else
                {
                    var idTipo = await _context.contas.Where(a => a.id.Equals(id)).Select(a => new { a.id, a.lancamento }).ToListAsync();
                    if ((ids.Count() != 0) && (idTipo[0].lancamento))
                    {
                        return Content("A conta pai não aceita contas filhas");
                    }
                    else {
                        return id + ".1";
                    }
                    
                }                
            }
        }

        private bool ContasExists(string id)
        {
            return _context.contas.Any(e => e.id == id);
        }
    }
}
