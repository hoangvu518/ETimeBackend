using Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Infrastructure.Configurations
{
    public class RequestConfiguration : IEntityTypeConfiguration<Request>
    {
        public void Configure(EntityTypeBuilder<Request> builder)
        {

            builder.Property(e => e.RequestDescription)
                    .IsRequired()
                    .HasMaxLength(256)
                    .IsUnicode(false);

            builder.Property(e => e.RequestTitle)
                    .IsRequired()
                    .HasMaxLength(256)
                    .IsUnicode(false);

            builder.HasOne(d => d.RequestType)
                    .WithMany(p => p.Request)
                    .HasForeignKey(d => d.RequestTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Request_RequestType");

            builder.HasOne(d => d.RequestedByEmployee)
                    .WithMany(p => p.Request)
                    .HasForeignKey(d => d.RequestedByEmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Request_Employee");

        }
    }
}
