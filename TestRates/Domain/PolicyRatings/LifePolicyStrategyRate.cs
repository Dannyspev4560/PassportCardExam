using System;
using System.Collections.Generic;
using System.Text;
using TestRating.Logger;

namespace TestRating.Domain.PolicyRatings
{
    public class LifePolicyStrategyRate : IPolicyStrategyRate
    {
        private const int BASE_DIVIDOR = 200;
        private const int SMOKER_RATE = 2;
        private readonly ILogger logger;
        public LifePolicyStrategyRate(ILogger logger)
        {
            this.logger = logger;
        }
        public decimal? Rate(Policy policy)
        {
            logger.Info("Rating LIFE policy...");
            logger.Info("Validating policy.");
            if (policy.DateOfBirth == DateTime.MinValue)
            {
                logger.Info("Life policy must include Date of Birth.");
                return null;
            }
            if (policy.DateOfBirth < DateTime.Today.AddYears(-100))
            {
                logger.Info("Max eligible age for coverage is 100 years.");
                return null;
            }
            if (policy.Amount == 0)
            {
                logger.Info("Life policy must include an Amount.");
                return null;
            }

            int age = DateTime.Today.Year - policy.DateOfBirth.Year;
            if (policy.DateOfBirth.Month == DateTime.Today.Month &&
                DateTime.Today.Day < policy.DateOfBirth.Day ||
                DateTime.Today.Month < policy.DateOfBirth.Month)
            {
                age--;
            }
            decimal baseRate = policy.Amount * age / BASE_DIVIDOR;

            if (policy.IsSmoker)
            {
                return baseRate * SMOKER_RATE;
            }
            return baseRate;
        }
    }
}
