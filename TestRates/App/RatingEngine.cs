using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.IO;
using TestRating.Domain;
using TestRating.Domain.Enums;
using TestRating.Domain.PolicyRatings;
using TestRating.Logger;

namespace TestRating.App
{
    /// <summary>
    /// The RatingEngine reads the policy application details from a file and produces a numeric 
    /// rating value based on the details.
    /// </summary>
    public class RatingEngine
    {
        private readonly ILogger logger;
        public RatingEngine(ILogger logger)
        {
            this.logger = logger;
        }
        public decimal Rating { get; set; }
        public void Rate(string filePath)
        {
            // log start rating
            logger.Info("Starting rate.");

            logger.Info("Loading policy.");

            // load policy - open file policy.json
            string policyJson = File.ReadAllText(filePath);


            var policy = JsonConvert.DeserializeObject<Policy>(policyJson,
                new StringEnumConverter());

            switch (policy.Type)
            {
                case PolicyType.Health:
                    policy.SetStrategyRate( new HealthPolicyStrategyRate(logger));
                    Rating = policy.ExecuteStrategyRate() ?? 0;
                    break;

                case PolicyType.Travel:
                    policy.SetStrategyRate(new TravelPolicyStrategyRate(logger));
                    Rating = policy.ExecuteStrategyRate() ?? 0;
                    break;

                case PolicyType.Life:
                    policy.SetStrategyRate(new LifePolicyStrategyRate(logger));
                    Rating = policy.ExecuteStrategyRate() ?? 0;
                    break;

                default:
                    logger.Info("Unknown policy type");
                    break;
            }

            logger.Info("Rating completed.");
        }
    }
}
