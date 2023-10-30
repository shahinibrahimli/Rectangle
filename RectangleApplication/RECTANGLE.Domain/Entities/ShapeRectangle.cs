using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Rectangle.Domain.Common; 

namespace Rectangle.Domain.Entities;

public class ShapeRectangle :  AuditableEntity
{

    public int Id { get; set; }
    public decimal Width { get; set; }
    public decimal Length { get; set; }

    public virtual OwnedRectangle OwnedRectangle { get; set; }

    public ShapeRectangle()
    { 
    } 
}