﻿namespace BeyondNet.Ddd.Es.ConsoleApp.Entities
{
    public class SampleEntityProps: IProps
    {
        public SampleName Name { get; private set; }
        public SampleEntityStatus Status { get; set; }
        public AuditValueObject Audit { get; private set; }


        public SampleEntityProps(SampleName name)
        {
            Name = name;
            Status = SampleEntityStatus.Active;
            Audit = AuditValueObject.Create("default");
        }

        public SampleEntityProps(SampleName name, SampleEntityStatus status, AuditValueObject audit)
        {
            Name = name;
            Status = status;
            Audit = audit;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }

    public class SampleEntity : Entity<SampleEntity, SampleEntityProps>
    {
        private SampleEntity(SampleEntityProps props) : base(props)
        {
        }

        public static SampleEntity Create(SampleName name)
        {
            var props = new SampleEntityProps(name);

            return new SampleEntity(props);
        }

        public static SampleEntity Load(string id, SampleEntityProps props)
        {
            var sampleEntity = new SampleEntity(props);
            sampleEntity.SetId(id);

            return sampleEntity;

        }

        public void ChangeName(StringValueObject name)
        {
            Props.Name.SetValue(name.GetValue());
            Props.Audit.Update("default");
        }

        public void Inactivate()
        {
            if (GetPropsCopy().Status == SampleEntityStatus.Inactive)
            {
                AddBrokenRule("Status", "The entity is already inactive");
                return;
            }

            Props.Status = SampleEntityStatus.Inactive;
            Props.Audit.Update("default");
        }

        public void Activate()
        {
            if (GetPropsCopy().Status == SampleEntityStatus.Active)
            {
                AddBrokenRule("Status", "The entity is already active");
                return;
            }

            Props.Status = SampleEntityStatus.Active;
            Props.Audit.Update("default");
        }

        public override void AddValidators()
        {
            AddValidator(new SampleEntityValidator(this));
        }
    }

    public class SampleEntityStatus : Enumeration
    {
        public static SampleEntityStatus Active = new SampleEntityStatus(1, "Active");
        public static SampleEntityStatus Inactive = new SampleEntityStatus(2, "Inactive");

        public SampleEntityStatus(int id, string name) : base(id, name)
        {
        }
    }
}
