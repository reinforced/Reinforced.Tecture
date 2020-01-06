using System;
using System.Collections.Generic;

namespace Reinforced.Tecture.Commands {
 
	public class ServicePipeline<T1, T2> 
		: ServicePipeline<T1>
	
	{
		internal ServicePipeline(Pipeline p) : base(p){}

		public override IEnumerable<Type> Subjects 
		{
			get 
			{
				yield return typeof(T1); 
				yield return typeof(T2); 
			}
		}

		public override bool IsSubject(Type t)
		{
			if (t==typeof(T1)) return true;
			if (t==typeof(T2)) return true; 
			return false;
		}
	}
 
	public class ServicePipeline<T1, T2, T3> 
		: ServicePipeline<T1, T2>
	
	{
		internal ServicePipeline(Pipeline p) : base(p){}

		public override IEnumerable<Type> Subjects 
		{
			get 
			{
				yield return typeof(T1); 
				yield return typeof(T2);  
				yield return typeof(T3); 
			}
		}

		public override bool IsSubject(Type t)
		{
			if (t==typeof(T1)) return true;
			if (t==typeof(T2)) return true; 
			if (t==typeof(T3)) return true; 
			return false;
		}
	}
 
	public class ServicePipeline<T1, T2, T3, T4> 
		: ServicePipeline<T1, T2, T3>
	
	{
		internal ServicePipeline(Pipeline p) : base(p){}

		public override IEnumerable<Type> Subjects 
		{
			get 
			{
				yield return typeof(T1); 
				yield return typeof(T2);  
				yield return typeof(T3);  
				yield return typeof(T4); 
			}
		}

		public override bool IsSubject(Type t)
		{
			if (t==typeof(T1)) return true;
			if (t==typeof(T2)) return true; 
			if (t==typeof(T3)) return true; 
			if (t==typeof(T4)) return true; 
			return false;
		}
	}
 
	public class ServicePipeline<T1, T2, T3, T4, T5> 
		: ServicePipeline<T1, T2, T3, T4>
	
	{
		internal ServicePipeline(Pipeline p) : base(p){}

		public override IEnumerable<Type> Subjects 
		{
			get 
			{
				yield return typeof(T1); 
				yield return typeof(T2);  
				yield return typeof(T3);  
				yield return typeof(T4);  
				yield return typeof(T5); 
			}
		}

		public override bool IsSubject(Type t)
		{
			if (t==typeof(T1)) return true;
			if (t==typeof(T2)) return true; 
			if (t==typeof(T3)) return true; 
			if (t==typeof(T4)) return true; 
			if (t==typeof(T5)) return true; 
			return false;
		}
	}
 
	public class ServicePipeline<T1, T2, T3, T4, T5, T6> 
		: ServicePipeline<T1, T2, T3, T4, T5>
	
	{
		internal ServicePipeline(Pipeline p) : base(p){}

		public override IEnumerable<Type> Subjects 
		{
			get 
			{
				yield return typeof(T1); 
				yield return typeof(T2);  
				yield return typeof(T3);  
				yield return typeof(T4);  
				yield return typeof(T5);  
				yield return typeof(T6); 
			}
		}

		public override bool IsSubject(Type t)
		{
			if (t==typeof(T1)) return true;
			if (t==typeof(T2)) return true; 
			if (t==typeof(T3)) return true; 
			if (t==typeof(T4)) return true; 
			if (t==typeof(T5)) return true; 
			if (t==typeof(T6)) return true; 
			return false;
		}
	}
 
	public class ServicePipeline<T1, T2, T3, T4, T5, T6, T7> 
		: ServicePipeline<T1, T2, T3, T4, T5, T6>
	
	{
		internal ServicePipeline(Pipeline p) : base(p){}

		public override IEnumerable<Type> Subjects 
		{
			get 
			{
				yield return typeof(T1); 
				yield return typeof(T2);  
				yield return typeof(T3);  
				yield return typeof(T4);  
				yield return typeof(T5);  
				yield return typeof(T6);  
				yield return typeof(T7); 
			}
		}

		public override bool IsSubject(Type t)
		{
			if (t==typeof(T1)) return true;
			if (t==typeof(T2)) return true; 
			if (t==typeof(T3)) return true; 
			if (t==typeof(T4)) return true; 
			if (t==typeof(T5)) return true; 
			if (t==typeof(T6)) return true; 
			if (t==typeof(T7)) return true; 
			return false;
		}
	}
 
	public class ServicePipeline<T1, T2, T3, T4, T5, T6, T7, T8> 
		: ServicePipeline<T1, T2, T3, T4, T5, T6, T7>
	
	{
		internal ServicePipeline(Pipeline p) : base(p){}

		public override IEnumerable<Type> Subjects 
		{
			get 
			{
				yield return typeof(T1); 
				yield return typeof(T2);  
				yield return typeof(T3);  
				yield return typeof(T4);  
				yield return typeof(T5);  
				yield return typeof(T6);  
				yield return typeof(T7);  
				yield return typeof(T8); 
			}
		}

		public override bool IsSubject(Type t)
		{
			if (t==typeof(T1)) return true;
			if (t==typeof(T2)) return true; 
			if (t==typeof(T3)) return true; 
			if (t==typeof(T4)) return true; 
			if (t==typeof(T5)) return true; 
			if (t==typeof(T6)) return true; 
			if (t==typeof(T7)) return true; 
			if (t==typeof(T8)) return true; 
			return false;
		}
	}
	
}