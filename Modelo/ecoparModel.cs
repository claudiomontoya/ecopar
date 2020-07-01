namespace Modelo
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ecoparModel : DbContext
    {
        public ecoparModel()
            : base("name=ecoparModel")
        {
        }

        public virtual DbSet<bodega> bodega { get; set; }
        public virtual DbSet<bodegasdestinos> bodegasdestinos { get; set; }
        public virtual DbSet<Cliente_Persona> Cliente_Persona { get; set; }
        public virtual DbSet<ClienteDestino> ClienteDestino { get; set; }
        public virtual DbSet<clientes> clientes { get; set; }
        public virtual DbSet<comunas> comunas { get; set; }
        public virtual DbSet<destino_persona> destino_persona { get; set; }
        public virtual DbSet<destinos> destinos { get; set; }
        public virtual DbSet<detalle_factura> detalle_factura { get; set; }
        public virtual DbSet<emision> emision { get; set; }
        public virtual DbSet<facturas> facturas { get; set; }
        public virtual DbSet<formapago> formapago { get; set; }
        public virtual DbSet<item> item { get; set; }
        public virtual DbSet<paises> paises { get; set; }
        public virtual DbSet<Personas> Personas { get; set; }
        public virtual DbSet<Productos> Productos { get; set; }
        public virtual DbSet<regiones> regiones { get; set; }
        public virtual DbSet<Solicitud> Solicitud { get; set; }
        public virtual DbSet<sucursal> sucursal { get; set; }
        public virtual DbSet<tipo_persona> tipo_persona { get; set; }
        public virtual DbSet<tranferencia_bodega> tranferencia_bodega { get; set; }
        public virtual DbSet<tranferencia_destino> tranferencia_destino { get; set; }
        public virtual DbSet<usuarios> usuarios { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<bodega>()
                .Property(e => e.descripcion)
                .IsUnicode(false);

            modelBuilder.Entity<bodega>()
                .Property(e => e.direccion)
                .IsUnicode(false);

            modelBuilder.Entity<bodega>()
                .Property(e => e.numero)
                .IsUnicode(false);

            modelBuilder.Entity<bodega>()
                .Property(e => e.telefono)
                .IsUnicode(false);

            modelBuilder.Entity<bodega>()
                .HasMany(e => e.Solicitud)
                .WithRequired(e => e.bodega)
                .HasForeignKey(e => e.id_bodega)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<bodegasdestinos>()
                .Property(e => e.descripcion)
                .IsUnicode(false);

            modelBuilder.Entity<bodegasdestinos>()
                .Property(e => e.direccion)
                .IsUnicode(false);

            modelBuilder.Entity<bodegasdestinos>()
                .Property(e => e.numero)
                .IsUnicode(false);

            modelBuilder.Entity<bodegasdestinos>()
                .Property(e => e.telefono)
                .IsUnicode(false);

            modelBuilder.Entity<bodegasdestinos>()
                .HasMany(e => e.tranferencia_destino)
                .WithRequired(e => e.bodegasdestinos)
                .HasForeignKey(e => e.id_destino)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ClienteDestino>()
                .Property(e => e.estado)
                .IsFixedLength();

            modelBuilder.Entity<clientes>()
                .Property(e => e.rut)
                .IsUnicode(false);

            modelBuilder.Entity<clientes>()
                .Property(e => e.nombre)
                .IsUnicode(false);

            modelBuilder.Entity<clientes>()
                .Property(e => e.direccion)
                .IsUnicode(false);

            modelBuilder.Entity<clientes>()
                .Property(e => e.numero)
                .IsUnicode(false);

            modelBuilder.Entity<clientes>()
                .Property(e => e.telefono)
                .IsUnicode(false);

            modelBuilder.Entity<clientes>()
                .Property(e => e.celular)
                .IsUnicode(false);

            modelBuilder.Entity<clientes>()
                .Property(e => e.correo)
                .IsUnicode(false);

            modelBuilder.Entity<clientes>()
                .Property(e => e.clave)
                .IsUnicode(false);

            modelBuilder.Entity<clientes>()
                .Property(e => e.descripcion)
                .IsUnicode(false);

            modelBuilder.Entity<clientes>()
                .HasMany(e => e.bodega)
                .WithRequired(e => e.clientes)
                .HasForeignKey(e => e.id_cliente)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<clientes>()
                .HasMany(e => e.Cliente_Persona)
                .WithRequired(e => e.clientes)
                .HasForeignKey(e => e.id_cliente)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<clientes>()
                .HasMany(e => e.ClienteDestino)
                .WithRequired(e => e.clientes)
                .HasForeignKey(e => e.id_cliente)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<clientes>()
                .HasMany(e => e.Solicitud)
                .WithRequired(e => e.clientes)
                .HasForeignKey(e => e.id_cliente)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<comunas>()
                .Property(e => e.comuna)
                .IsUnicode(false);

            modelBuilder.Entity<comunas>()
                .HasMany(e => e.bodega)
                .WithRequired(e => e.comunas)
                .HasForeignKey(e => e.id_comuna)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<comunas>()
                .HasMany(e => e.bodegasdestinos)
                .WithRequired(e => e.comunas)
                .HasForeignKey(e => e.id_comuna)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<comunas>()
                .HasMany(e => e.clientes)
                .WithRequired(e => e.comunas)
                .HasForeignKey(e => e.id_comuna)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<comunas>()
                .HasMany(e => e.destinos)
                .WithRequired(e => e.comunas)
                .HasForeignKey(e => e.id_comuna)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<comunas>()
                .HasMany(e => e.sucursal)
                .WithRequired(e => e.comunas)
                .HasForeignKey(e => e.id_comuna)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<destinos>()
                .Property(e => e.nombre)
                .IsUnicode(false);

            modelBuilder.Entity<destinos>()
                .Property(e => e.direccion)
                .IsUnicode(false);

            modelBuilder.Entity<destinos>()
                .Property(e => e.telefono)
                .IsUnicode(false);

            modelBuilder.Entity<destinos>()
                .Property(e => e.mail)
                .IsUnicode(false);

            modelBuilder.Entity<destinos>()
                .HasMany(e => e.bodegasdestinos)
                .WithRequired(e => e.destinos)
                .HasForeignKey(e => e.id_destino)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<destinos>()
                .HasMany(e => e.ClienteDestino)
                .WithRequired(e => e.destinos)
                .HasForeignKey(e => e.id_destino)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<destinos>()
                .HasMany(e => e.destino_persona)
                .WithRequired(e => e.destinos)
                .HasForeignKey(e => e.id_destino)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<emision>()
                .Property(e => e.usuarioeco)
                .IsUnicode(false);

            modelBuilder.Entity<emision>()
                .Property(e => e.estado)
                .IsUnicode(false);

            modelBuilder.Entity<emision>()
                .HasMany(e => e.tranferencia_bodega)
                .WithRequired(e => e.emision)
                .HasForeignKey(e => e.id_emision)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<emision>()
                .HasMany(e => e.tranferencia_destino)
                .WithRequired(e => e.emision)
                .HasForeignKey(e => e.id_emision)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<facturas>()
                .Property(e => e.detalle)
                .IsUnicode(false);

            modelBuilder.Entity<facturas>()
                .HasMany(e => e.detalle_factura)
                .WithOptional(e => e.facturas)
                .HasForeignKey(e => e.id_factura);

            modelBuilder.Entity<formapago>()
                .Property(e => e.nombre)
                .IsUnicode(false);

            modelBuilder.Entity<formapago>()
                .HasMany(e => e.clientes)
                .WithRequired(e => e.formapago)
                .HasForeignKey(e => e.id_formaPago)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<item>()
                .Property(e => e.nombre)
                .IsUnicode(false);

            modelBuilder.Entity<item>()
                .HasMany(e => e.detalle_factura)
                .WithOptional(e => e.item)
                .HasForeignKey(e => e.id_item);

            modelBuilder.Entity<paises>()
                .Property(e => e.pais)
                .IsUnicode(false);

            modelBuilder.Entity<paises>()
                .HasMany(e => e.regiones)
                .WithRequired(e => e.paises)
                .HasForeignKey(e => e.id_pais)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Personas>()
                .Property(e => e.rut)
                .IsUnicode(false);

            modelBuilder.Entity<Personas>()
                .Property(e => e.nombre)
                .IsUnicode(false);

            modelBuilder.Entity<Personas>()
                .Property(e => e.apellido)
                .IsUnicode(false);

            modelBuilder.Entity<Personas>()
                .Property(e => e.telefono)
                .IsUnicode(false);

            modelBuilder.Entity<Personas>()
                .Property(e => e.celular)
                .IsUnicode(false);

            modelBuilder.Entity<Personas>()
                .Property(e => e.mail)
                .IsUnicode(false);

            modelBuilder.Entity<Personas>()
                .HasMany(e => e.Cliente_Persona)
                .WithRequired(e => e.Personas)
                .HasForeignKey(e => e.id_persona)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Personas>()
                .HasMany(e => e.ClienteDestino)
                .WithRequired(e => e.Personas)
                .HasForeignKey(e => e.id_contacto)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Personas>()
                .HasMany(e => e.destino_persona)
                .WithRequired(e => e.Personas)
                .HasForeignKey(e => e.id_persona)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Productos>()
                .Property(e => e.producto)
                .IsUnicode(false);

            modelBuilder.Entity<Productos>()
                .Property(e => e.descripcion)
                .IsUnicode(false);

            modelBuilder.Entity<Productos>()
                .HasMany(e => e.Solicitud)
                .WithRequired(e => e.Productos)
                .HasForeignKey(e => e.id_producto)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<regiones>()
                .Property(e => e.region)
                .IsUnicode(false);

            modelBuilder.Entity<regiones>()
                .HasMany(e => e.comunas)
                .WithRequired(e => e.regiones)
                .HasForeignKey(e => e.id_region)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Solicitud>()
                .Property(e => e.usuario)
                .IsUnicode(false);

            modelBuilder.Entity<Solicitud>()
                .Property(e => e.estado)
                .IsUnicode(false);

            modelBuilder.Entity<Solicitud>()
                .Property(e => e.descripcion)
                .IsUnicode(false);

            modelBuilder.Entity<Solicitud>()
                .Property(e => e.archivo)
                .IsUnicode(false);

            modelBuilder.Entity<Solicitud>()
                .Property(e => e.tipo_despacho)
                .IsUnicode(false);

            modelBuilder.Entity<Solicitud>()
                .HasMany(e => e.emision)
                .WithRequired(e => e.Solicitud)
                .HasForeignKey(e => e.id_solicitud)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Solicitud>()
                .HasMany(e => e.facturas)
                .WithOptional(e => e.Solicitud)
                .HasForeignKey(e => e.id_solicitud);

            modelBuilder.Entity<sucursal>()
                .Property(e => e.nombre)
                .IsUnicode(false);

            modelBuilder.Entity<sucursal>()
                .Property(e => e.direccion)
                .IsUnicode(false);

            modelBuilder.Entity<sucursal>()
                .Property(e => e.numero)
                .IsUnicode(false);

            modelBuilder.Entity<sucursal>()
                .Property(e => e.telefono)
                .IsUnicode(false);

            modelBuilder.Entity<sucursal>()
                .Property(e => e.celular)
                .IsUnicode(false);

            modelBuilder.Entity<sucursal>()
                .HasMany(e => e.Solicitud)
                .WithRequired(e => e.sucursal)
                .HasForeignKey(e => e.id_sucursal)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<sucursal>()
                .HasMany(e => e.tranferencia_bodega)
                .WithRequired(e => e.sucursal)
                .HasForeignKey(e => e.id_sucursal)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tipo_persona>()
                .Property(e => e.nombre)
                .IsUnicode(false);

            modelBuilder.Entity<tipo_persona>()
                .HasMany(e => e.Cliente_Persona)
                .WithRequired(e => e.tipo_persona)
                .HasForeignKey(e => e.id_tipo)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tipo_persona>()
                .HasMany(e => e.destino_persona)
                .WithRequired(e => e.tipo_persona)
                .HasForeignKey(e => e.id_tipo)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tranferencia_bodega>()
                .Property(e => e.usuario)
                .IsUnicode(false);

            modelBuilder.Entity<tranferencia_bodega>()
                .Property(e => e.archivo)
                .IsUnicode(false);

            modelBuilder.Entity<tranferencia_bodega>()
                .Property(e => e.estado)
                .IsUnicode(false);

            modelBuilder.Entity<tranferencia_bodega>()
                .Property(e => e.tipo)
                .IsUnicode(false);

            modelBuilder.Entity<tranferencia_destino>()
                .Property(e => e.usuario)
                .IsUnicode(false);

            modelBuilder.Entity<tranferencia_destino>()
                .Property(e => e.archivo)
                .IsUnicode(false);

            modelBuilder.Entity<tranferencia_destino>()
                .Property(e => e.estado)
                .IsUnicode(false);

            modelBuilder.Entity<tranferencia_destino>()
                .Property(e => e.tipo)
                .IsUnicode(false);

            modelBuilder.Entity<usuarios>()
                .Property(e => e.usuario)
                .IsUnicode(false);

            modelBuilder.Entity<usuarios>()
                .Property(e => e.nombre)
                .IsUnicode(false);

            modelBuilder.Entity<usuarios>()
                .Property(e => e.clave)
                .IsUnicode(false);

            modelBuilder.Entity<usuarios>()
                .Property(e => e.mail)
                .IsUnicode(false);

            modelBuilder.Entity<usuarios>()
                .HasMany(e => e.Solicitud)
                .WithRequired(e => e.usuarios)
                .WillCascadeOnDelete(false);
        }
    }
}
