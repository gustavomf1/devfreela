using DevFreela.Core.Entities;

namespace DevFreela.Application.Models
{
    public class ProjectItemViwModel
    {
        public ProjectItemViwModel(int id, string title, string clientName, string freelancerName, decimal totalCost)
        {
            Id = id;
            Title = title;
            ClientName = clientName;
            FreelancerName = freelancerName;
            TotalCost = totalCost;
        }

        public int Id { get; private set; }
        public string Title { get; private set; }
        public string ClientName { get; private set; }
        public string FreelancerName { get; private set; }
        public decimal TotalCost { get; private set; }

        public static ProjectItemViwModel FromEntity(Project entity) => new ProjectItemViwModel(entity.Id, entity.Title, entity.Client.FullName, entity.Freelancer.FullName, entity.TotalCost);
    }
}
