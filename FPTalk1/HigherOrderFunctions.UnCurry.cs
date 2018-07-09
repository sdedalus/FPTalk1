namespace FPTalk1
{
	using System;

	public static partial class HigherOrderFunctions
	{
		public static Func<T1, T2, R> UnCurry<T1, T2, R>(this Func<T1, Func<T2, R>> function) => (a, b) => function(a)(b);

		public static Func<T1, T2, T3, R> UnCurry<T1, T2, T3, R>(this Func<T1, Func<T2, Func<T3, R>>> function) => (a, b, c) => function(a)(b)(c);

		public static Func<T1, T2, T3, T4, R> UnCurry<T1, T2, T3, T4, R>(this Func<T1, Func<T2, Func<T3, Func<T4, R>>>> function) => (a, b, c, d) => function(a)(b)(c)(d);

		public static Func<T1, T2, T3, T4, T5, R> UnCurry<T1, T2, T3, T4, T5, R>(this Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, R>>>>> function) => (a, b, c, d, e) => function(a)(b)(c)(d)(e);

		public static Func<T1, T2, T3, T4, T5, T6, R> UnCurry<T1, T2, T3, T4, T5, T6, R>(this Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Func<T6, R>>>>>> function) => (a, b, c, d, e, f) => function(a)(b)(c)(d)(e)(f);

		public static Func<T1, T2, T3, T4, T5, T6, T7, R> UnCurry<T1, T2, T3, T4, T5, T6, T7, R>(this Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Func<T6, Func<T7, R>>>>>>> function) => (a, b, c, d, e, f, g) => function(a)(b)(c)(d)(e)(f)(g);

		public static Func<T1, T2, T3, T4, T5, T6, T7, T8, R> UnCurry<T1, T2, T3, T4, T5, T6, T7, T8, R>(this Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Func<T6, Func<T7, Func<T8, R>>>>>>>> function) => (a, b, c, d, e, f, g, h) => function(a)(b)(c)(d)(e)(f)(g)(h);
	}
}
