﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Models
{
    public class Comment
    {
        [Key]
        [Column("CommentId")]
        public Guid CommentId { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
        public string Text { get; set; }

        public Guid ProductId { get; set; }
        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; }

        public string Id { get; set; }
        [ForeignKey(nameof(Id))]
        public User User { get; set; }
    }
}
