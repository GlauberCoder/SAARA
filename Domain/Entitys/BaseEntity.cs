using Domain.Abstractions.Entitys;

namespace Domain.Entitys
{
	public abstract class BaseEntity<T> :
		IBaseEntity,
		IEquatable<BaseEntity<T>>,
		IComparer<BaseEntity<T>>,
		IEqualityComparer<BaseEntity<T>>,
		IBaseEntity<T>
		where T : class
	{
		public BaseEntity()
		{
			ID = InstanceID;
		}
		public virtual DateTime? Exclusion { get; set; }
		public virtual long ID { get; set; }
		public virtual string Name { get; set; }
		public virtual bool Excluded => Exclusion.HasValue;
		public virtual bool Saved => ID != InstanceID;
		public virtual bool Saving => ID == InstanceID;
		public virtual long InstanceID => 0;

		public static bool operator ==(BaseEntity<T> x, BaseEntity<T> y)
		{
			if (Object.ReferenceEquals(x, null) || Object.ReferenceEquals(y, null))
				return Object.ReferenceEquals(x, null) && Object.ReferenceEquals(y, null);
			else
			{
				var equalTypes = x.GetType() == y.GetType() || x.GetType().Name.Replace("Proxy", String.Empty) == y.GetType().Name.Replace("Proxy", String.Empty);
				if (x.ID != 0)
					return x.ID == y.ID && equalTypes;
				else
					return x.GetHashCode() == y.GetHashCode() && x.ID == y.ID && equalTypes;
			}
		}
		public override bool Equals(object obj) => obj is BaseEntity<T> ? ((BaseEntity<T>)obj) == this : base.Equals(obj);
		public static bool operator !=(BaseEntity<T> x, BaseEntity<T> y) => !(x == y);
		public virtual string Identification() => Name;
		public virtual bool Equals(IBaseEntity other) => other.ID == ID;
		public virtual bool Equals(BaseEntity<T> other) => other.ID == ID;
		public virtual bool Equals(IBaseEntity x, IBaseEntity y) => x == y;
		public virtual bool Equals(BaseEntity<T> x, BaseEntity<T> y) => x == y;
		public virtual int Compare(BaseEntity<T> x, BaseEntity<T> y) => x == y ? 0 : 1;
		public virtual int GetHashCode(IBaseEntity obj) => obj.ID.GetHashCode();
		public virtual int GetHashCode(BaseEntity<T> obj) => obj.ID.GetHashCode();
		public override int GetHashCode() => ((IBaseEntity)this).ID.GetHashCode();
		public virtual int Compare(IBaseEntity x, IBaseEntity y) => x == y? 0 : 1;
		public virtual object Simplify() => new { id = ID, name = Identification() };
		public virtual void Normalize()
		{

		}

		
	}
}
