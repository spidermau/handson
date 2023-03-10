// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using handson.Models;

#nullable disable

namespace handson.Migrations
{
    [DbContext(typeof(Context))]
    partial class ContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("handson.Models.Contas", b =>
                {
                    b.Property<string>("id")
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("codigo");

                    b.Property<string>("id_pai")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("codigo_pai");

                    b.Property<bool>("lancamento")
                        .HasColumnType("bit")
                        .HasColumnName("lancamento");

                    b.Property<string>("nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("nome");

                    b.Property<string>("tipo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("tipo");

                    b.HasKey("id");

                    b.ToTable("contas");
                });
#pragma warning restore 612, 618
        }
    }
}
