﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Neutrinodevs.PedidosOnline.Infraestructure.Models
{
    public partial class ND_PEDIDOS_ONLINEContext : DbContext
    {
        public ND_PEDIDOS_ONLINEContext()
        {
        }

        public ND_PEDIDOS_ONLINEContext(DbContextOptions<ND_PEDIDOS_ONLINEContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Clientes> Clientes { get; set; }
        public virtual DbSet<ComprobanteDetalle> ComprobanteDetalle { get; set; }
        public virtual DbSet<ComprobanteVenta> ComprobanteVenta { get; set; }
        public virtual DbSet<Empleados> Empleados { get; set; }
        public virtual DbSet<Mesas> Mesas { get; set; }
        public virtual DbSet<PedidoDetalle> PedidoDetalle { get; set; }
        public virtual DbSet<Pedidos> Pedidos { get; set; }
        public virtual DbSet<Productos> Productos { get; set; }
        public virtual DbSet<Secuenciales> Secuenciales { get; set; }
        public virtual DbSet<Usuarios> Usuarios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=BDUARTE-LAP; Initial Catalog=ND_PEDIDOS_ONLINE; user id=sa; password=12345678");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Clientes>(entity =>
            {
                entity.HasKey(e => e.IdCliente);

                entity.ToTable("CLIENTES");

                entity.Property(e => e.IdCliente).HasColumnName("id_cliente");

                entity.Property(e => e.Apellidos)
                    .IsRequired()
                    .HasColumnName("apellidos")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.CodigoVerificacion)
                    .HasColumnName("codigo_verificacion")
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.Direccion)
                    .HasColumnName("direccion")
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Estado).HasColumnName("estado");

                entity.Property(e => e.Etapa).HasColumnName("etapa");

                entity.Property(e => e.FechaVerificacion)
                    .HasColumnName("fecha_verificacion")
                    .HasColumnType("datetime");

                entity.Property(e => e.Identificacion)
                    .IsRequired()
                    .HasColumnName("identificacion")
                    .HasMaxLength(13)
                    .IsUnicode(false);

                entity.Property(e => e.Nombres)
                    .IsRequired()
                    .HasColumnName("nombres")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Telefono)
                    .HasColumnName("telefono")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.TipoCliente).HasColumnName("tipo_cliente");

                entity.Property(e => e.UsuarioId).HasColumnName("usuario_id");

                entity.HasOne(d => d.Usuario)
                    .WithMany(p => p.Clientes)
                    .HasForeignKey(d => d.UsuarioId)
                    .HasConstraintName("FK_CLIENTES_USUARIOS");
            });

            modelBuilder.Entity<ComprobanteDetalle>(entity =>
            {
                entity.HasKey(e => e.IdComprobanteDet);

                entity.ToTable("COMPROBANTE_DETALLE");

                entity.Property(e => e.IdComprobanteDet).HasColumnName("id_comprobante_det");

                entity.Property(e => e.Cantidad).HasColumnName("cantidad");

                entity.Property(e => e.ComprobanteId).HasColumnName("comprobante_id");

                entity.Property(e => e.Estado).HasColumnName("estado");

                entity.Property(e => e.ItemId).HasColumnName("item_id");

                entity.Property(e => e.Precio)
                    .HasColumnName("precio")
                    .HasColumnType("decimal(10, 2)");

                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.Property(e => e.Total)
                    .HasColumnName("total")
                    .HasColumnType("decimal(10, 2)");

                entity.HasOne(d => d.Comprobante)
                    .WithMany(p => p.ComprobanteDetalle)
                    .HasForeignKey(d => d.ComprobanteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_COMPROBANTE_DETALLE_COMPROBANTE_DETALLE");
            });

            modelBuilder.Entity<ComprobanteVenta>(entity =>
            {
                entity.HasKey(e => e.IdComprobante);

                entity.ToTable("COMPROBANTE_VENTA");

                entity.Property(e => e.IdComprobante).HasColumnName("id_comprobante");

                entity.Property(e => e.EsPos).HasColumnName("es_pos");

                entity.Property(e => e.Estado).HasColumnName("estado");

                entity.Property(e => e.Fecha)
                    .HasColumnName("fecha")
                    .HasColumnType("datetime");

                entity.Property(e => e.Iva)
                    .HasColumnName("iva")
                    .HasColumnType("decimal(10, 2)");

                entity.Property(e => e.Numero).HasColumnName("numero");

                entity.Property(e => e.Subtotal)
                    .HasColumnName("subtotal")
                    .HasColumnType("decimal(10, 2)");

                entity.Property(e => e.Total)
                    .HasColumnName("total")
                    .HasColumnType("decimal(12, 2)");
            });

            modelBuilder.Entity<Empleados>(entity =>
            {
                entity.HasKey(e => e.IdEmpleado);

                entity.ToTable("EMPLEADOS");

                entity.Property(e => e.IdEmpleado).HasColumnName("id_empleado");

                entity.Property(e => e.Apellidos)
                    .IsRequired()
                    .HasColumnName("apellidos")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Estado).HasColumnName("estado");

                entity.Property(e => e.EstadoActividad).HasColumnName("estado_actividad");

                entity.Property(e => e.FechaCreacion)
                    .HasColumnName("fecha_creacion")
                    .HasColumnType("datetime");

                entity.Property(e => e.FechaEliminacion)
                    .HasColumnName("fecha_eliminacion")
                    .HasColumnType("datetime");

                entity.Property(e => e.Identificacion)
                    .IsRequired()
                    .HasColumnName("identificacion")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Nombres)
                    .IsRequired()
                    .HasColumnName("nombres")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.TipoEmpleado).HasColumnName("tipo_empleado");

                entity.Property(e => e.UsuarioId).HasColumnName("usuario_id");

                entity.HasOne(d => d.Usuario)
                    .WithMany(p => p.Empleados)
                    .HasForeignKey(d => d.UsuarioId)
                    .HasConstraintName("FK_EMPLEADOS_USUARIOS");
            });

            modelBuilder.Entity<Mesas>(entity =>
            {
                entity.HasKey(e => e.IdMesa);

                entity.ToTable("MESAS");

                entity.Property(e => e.IdMesa).HasColumnName("id_mesa");

                entity.Property(e => e.Estado).HasColumnName("estado");

                entity.Property(e => e.Nombre)
                    .HasColumnName("nombre")
                    .HasMaxLength(40)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PedidoDetalle>(entity =>
            {
                entity.HasKey(e => e.IdPedidoDet);

                entity.ToTable("PEDIDO_DETALLE");

                entity.Property(e => e.IdPedidoDet).HasColumnName("id_pedido_det");

                entity.Property(e => e.Cantidad).HasColumnName("cantidad");

                entity.Property(e => e.Estado).HasColumnName("estado");

                entity.Property(e => e.NombreProducto)
                    .IsRequired()
                    .HasColumnName("nombre_producto")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PedidoId).HasColumnName("pedido_id");

                entity.Property(e => e.ProductoId).HasColumnName("producto_id");

                entity.Property(e => e.Total)
                    .HasColumnName("total")
                    .HasColumnType("decimal(10, 2)");

                entity.HasOne(d => d.Pedido)
                    .WithMany(p => p.PedidoDetalle)
                    .HasForeignKey(d => d.PedidoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PEDIDO_DETALLE_PEDIDOS");

                entity.HasOne(d => d.Producto)
                    .WithMany(p => p.PedidoDetalle)
                    .HasForeignKey(d => d.ProductoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PEDIDO_DETALLE_PRODUCTOS");
            });

            modelBuilder.Entity<Pedidos>(entity =>
            {
                entity.HasKey(e => e.IdPedido);

                entity.ToTable("PEDIDOS");

                entity.Property(e => e.IdPedido).HasColumnName("id_pedido");

                entity.Property(e => e.ClienteId).HasColumnName("cliente_id");

                entity.Property(e => e.DeliveryId).HasColumnName("delivery_id");

                entity.Property(e => e.DeliveryTime)
                    .HasColumnName("delivery_time")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.Estado).HasColumnName("estado");

                entity.Property(e => e.Fecha)
                    .HasColumnName("fecha")
                    .HasColumnType("datetime");

                entity.Property(e => e.Iva)
                    .HasColumnName("iva")
                    .HasColumnType("decimal(10, 2)");

                entity.Property(e => e.MotivoRechazo)
                    .HasColumnName("motivo_rechazo")
                    .IsUnicode(false);

                entity.Property(e => e.Numero).HasColumnName("numero");

                entity.Property(e => e.Observaciones)
                    .HasColumnName("observaciones")
                    .IsUnicode(false);

                entity.Property(e => e.Stage).HasColumnName("stage");

                entity.Property(e => e.Subtotal)
                    .HasColumnName("subtotal")
                    .HasColumnType("decimal(10, 2)");

                entity.Property(e => e.Total)
                    .HasColumnName("total")
                    .HasColumnType("decimal(10, 2)");

                entity.HasOne(d => d.Cliente)
                    .WithMany(p => p.Pedidos)
                    .HasForeignKey(d => d.ClienteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PEDIDOS_PEDIDOS");
            });

            modelBuilder.Entity<Productos>(entity =>
            {
                entity.HasKey(e => e.IdProducto);

                entity.ToTable("PRODUCTOS");

                entity.Property(e => e.IdProducto).HasColumnName("id_producto");

                entity.Property(e => e.Estado).HasColumnName("estado");

                entity.Property(e => e.Imagen)
                    .HasColumnName("imagen")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnName("nombre")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Precio)
                    .HasColumnName("precio")
                    .HasColumnType("decimal(10, 2)");

                entity.Property(e => e.Tipo).HasColumnName("tipo");
            });

            modelBuilder.Entity<Secuenciales>(entity =>
            {
                entity.HasKey(e => e.IdSecuencial);

                entity.ToTable("SECUENCIALES");

                entity.Property(e => e.IdSecuencial).HasColumnName("id_secuencial");

                entity.Property(e => e.Nombre)
                    .HasColumnName("nombre")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Secuencial).HasColumnName("secuencial");
            });

            modelBuilder.Entity<Usuarios>(entity =>
            {
                entity.HasKey(e => e.IdUsuario);

                entity.ToTable("USUARIOS");

                entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Estado).HasColumnName("estado");

                entity.Property(e => e.Nombres)
                    .IsRequired()
                    .HasColumnName("nombres")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.TipoUsuario).HasColumnName("tipo_usuario");

                entity.Property(e => e.Username)
                    .HasColumnName("username")
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });
        }
    }
}
