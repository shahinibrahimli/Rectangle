using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Rectangle.Domain.Common; 

namespace Rectangle.Domain.Entities;

public class OwnedRectangle :  AuditableEntity
{

    public int Id { get; set; }
    public virtual List<ShapeRectangle> Rectangles { get; set; }

    public OwnedRectangle()
    { 
    } 
}