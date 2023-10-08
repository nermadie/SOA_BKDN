using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StudentMVC.Models
{
  public class Student
  {
    [Key]
    public int Id { get; set; }
    [StringLength(100)]
    public string Name { get; set; } = null!;
    public int Age { get; set; }
    [StringLength(256)]
    public String Address { get; set; } = null!;
  }
}