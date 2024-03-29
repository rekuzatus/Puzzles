﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Puzzles.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public string Email { get; set; }
        public string USerId { get; set; }
        [ForeignKey(nameof(USerId))]
        public ApplicationUser User { get; set; }
        public List<OrderItem> OrderItems { get; set; }
    }
}
