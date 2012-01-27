using Zymurgy.Dymensions;

namespace BrewRoom.Modules.Core.Interfaces.Models
{
    public interface IRecipeFermentable
    {
        string Name { get; }
        Weight Weight { get; set; }
        decimal Pppg { get; set; }
        decimal ExtractPoints { get; }
        decimal GravityContribution { get; }
        decimal GravityContributionInPoints { get; }
        decimal PercentageOfMash { get; }
        
        void IncreaseWeight(Weight weightToAdd);
    }
}