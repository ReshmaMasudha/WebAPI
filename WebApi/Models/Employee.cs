using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class Employee
    {
        [Key]
        [Required(ErrorMessage = "EmployeeId is required")]
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        public int ManagerId { get; set; }

        //public int Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        //public bool IsTransient()
        //{
        //    throw new NotImplementedException();
        //}
    }
}
