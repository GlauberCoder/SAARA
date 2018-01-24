using Domain.Abstractions;
using Domain.Abstractions.Entitys;
using System;
using System.Collections.Generic;
using System.Text;

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
		public virtual bool Excluded { get { return Exclusion.HasValue; } }
		public virtual bool Saved { get { return ID != InstanceID; } }
		public virtual bool Saving { get { return ID == InstanceID; } }
		public virtual DateTime? Exclusion { get; set; }
		public virtual long ID { get; set; }
		public virtual long InstanceID { get { return 0; } }
		public virtual string Name { get; set; }

		public virtual void Normalize()
		{

		}
		public virtual string Identification()
		{
			return Name;
		}
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
		public static bool operator !=(BaseEntity<T> x, BaseEntity<T> y)
		{
			return !(x == y);
		}
		public override bool Equals(object obj)
		{
			if (obj is BaseEntity<T>)
				return ((BaseEntity<T>)obj) == this;
			else
				return base.Equals(obj);
		}
		public virtual bool Equals(IBaseEntity other)
		{
			return other.ID == ID;
		}
		public virtual bool Equals(BaseEntity<T> other)
		{
			return other.ID == ID;
		}

		public virtual bool Equals(IBaseEntity x, IBaseEntity y)
		{
			return x == y;
		}
		public virtual bool Equals(BaseEntity<T> x, BaseEntity<T> y)
		{
			return x == y;
		}
		public virtual int Compare(BaseEntity<T> x, BaseEntity<T> y)
		{
			return x == y ? 0 : 1;
		}
		public virtual int GetHashCode(IBaseEntity obj)
		{
			return obj.ID.GetHashCode();
		}
		public virtual int GetHashCode(BaseEntity<T> obj)
		{
			return obj.ID.GetHashCode();
		}
		public override int GetHashCode()
		{
			return ((IBaseEntity)this).ID.GetHashCode();
		}
		public virtual int Compare(IBaseEntity x, IBaseEntity y)
		{
			return x == y ? 0 : 1;
		}
		public virtual object Simplify()
		{
			return new { id = ID, name = Identification() };
		}
		public BaseEntity()
		{
			ID = InstanceID;
		}
		
	}
}
