using BeyondNet.Ddd.Rules;
using BeyondNet.Ddd.Rules.Impl;

namespace BeyondNet.Ddd.Es.Domain.Entities
{
    public class SampleEntityValidator : AbstractRuleValidator<SampleEntity>
    {
        public SampleEntityValidator(SampleEntity subject) : base(subject)
        {
        }

        public override void AddRules(RuleContext? context)
        {
            var props = Subject.Props;

            var createdDate = props.Audit.GetValue().CreatedAt;

            if (IsWeekend(createdDate))
            {
                AddBrokenRule("CreatedAt", $"The created date {createdDate.ToShortDateString()} cannot be in the weekend.");
                return;
            }
        }

        public static bool IsWeekend(DateTime date)
        {
            return date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday;
        }

    }
}
