

namespace TestRating.Domain.PolicyRatings
{
    public interface IPolicyStrategyRate
    {
        public decimal? Rate(Policy policy);
    }
}
