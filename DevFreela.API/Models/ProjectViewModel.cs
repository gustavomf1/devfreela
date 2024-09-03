﻿using DevFreela.API.Entities;

namespace DevFreela.API.Models
{
    public class ProjectViewModel
    {
        public ProjectViewModel(int id, string title, string description, int idCliente, int idFreelancer, string clientName, string freelancerName, decimal totalCost, List<ProjectComment> comments)
        {
            Id = id;
            Title = title;
            Description = description;
            IdCliente = idCliente;
            IdFreelancer = idFreelancer;
            ClientName = clientName;
            FreelancerName = freelancerName;
            TotalCost = totalCost;
            Comments = comments?.Select(c => c.Content ?? string.Empty).ToList() ?? new List<string>();
        }

        public int Id { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public int IdCliente { get; private set; }
        public int IdFreelancer { get; private set; }
        public string ClientName { get; private set; }
        public string FreelancerName { get; private set; }
        public decimal TotalCost { get; private set; }
        public List<string> Comments { get; private set; }

        public static ProjectViewModel FromEntity(Project entity) => new ProjectViewModel(entity.Id, entity.Title, entity.Description, 
            entity.IdCliente, entity.IdFreelancer, entity.Client.FullName,
            entity.Freelancer.FullName, entity.TotalCost, entity.Comments);
        
    }
}
