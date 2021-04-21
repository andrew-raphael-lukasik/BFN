// src* = https://github.com/andrew-raphael-lukasik/BFN
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace BFN_Tests
{

	public class equality
	{
		[Test] public void _default__default () => Assert.AreEqual( expected:default(BFN) , actual:default(BFN) );
		[Test] public void _1e3__1000e0 () => Assert.AreEqual( expected:new BFN(1,3) , actual:new BFN(1000,0) );
	}

	public class hashcodes
	{
		[Test] public void _default__default () => Assert.AreEqual( expected:default(BFN).GetHashCode() , actual:default(BFN).GetHashCode() );
		[Test] public void _1e3__1000e0 () => Assert.AreEqual( expected:new BFN(1,3).GetHashCode() , actual:new BFN(1000,0).GetHashCode() );
		[Test] public void _12345e12345__12345e12345 () => Assert.AreEqual( expected:new BFN(12345,12345).GetHashCode() , actual:new BFN(12345,12345).GetHashCode() );
		[Test] public void _123e123__123e123 () => Assert.AreEqual( expected:new BFN(123,123).GetHashCode() , actual:new BFN(123,123).GetHashCode() );
		[Test] public void _n123en123__n123en123 () => Assert.AreEqual( expected:new BFN(-123,-123).GetHashCode() , actual:new BFN(-123,-123).GetHashCode() );
		[Test] public void _123en123__123en123 () => Assert.AreEqual( expected:new BFN(123,-123).GetHashCode() , actual:new BFN(123,-123).GetHashCode() );
		[Test] public void _n123e123__n123e123 () => Assert.AreEqual( expected:new BFN(-123,123).GetHashCode() , actual:new BFN(-123,123).GetHashCode() );
	}

	public class compression
	{
		[Test] public void _positive_exponent_1 () => Assert.AreEqual( expected:new BFN(12.345678910111213141,18) , actual:new BFN(12345678910111213141,0).compressed );
		[Test] public void _positive_exponent_2 () => Assert.AreEqual( expected:new BFN(9.999999999999999999,18) , actual:new BFN(9999999999999999999,0).compressed );
		[Test] public void _negative_exponent_1 () => Assert.AreEqual( expected:new BFN(0.01,-3) , actual:new BFN(0.00001,0).compressed );
		[Test] public void _0e0 () => Assert.AreEqual( expected:new BFN(0,0) , actual:new BFN(0,0).compressed );
		[Test] public void _123e123 () => Assert.AreEqual( expected:new BFN(123,123) , actual:new BFN(123,123).compressed );
		[Test] public void _default () => Assert.AreEqual( expected:default(BFN) , actual:default(BFN).compressed );
		[Test] public void _1e0 () => Assert.AreEqual( expected:new BFN(1,0) , actual:new BFN(1,0).compressed );
	}
	
	public class addition
	{
		[Test] public void _1e4__2e3 () => Assert.AreEqual( expected:new BFN(12,3) , actual:new BFN(1,4) + new BFN(2,3) );
		[Test] public void _9dot87654321e33__1dot23456789e14 () => Assert.AreEqual( expected:new BFN(9.87654321,33) , actual:new BFN(9.87654321,33) + new BFN(1.23456789,14) );
		[Test] public void _3e10__4e3 () => Assert.AreEqual( expected:new BFN(30.000004,9) , actual:new BFN(3,10) + new BFN(4,3) );
		[Test] public void _1e14__0e13 () => Assert.AreEqual( expected:new BFN(1,14) , actual:new BFN(1,14) + new BFN(0,13) );
		[Test] public void _1e14__0en13 () => Assert.AreEqual( expected:new BFN(1,14) , actual:new BFN(1,14) + new BFN(0,-13) );
		[Test] public void _n1en14__0en13 () => Assert.AreEqual( expected:new BFN(-1,-14) , actual:new BFN(-1,-14) + new BFN(0,-13) );
		[Test] public void _0e0__0e0 () => Assert.AreEqual( expected:new BFN(0,0) , actual:new BFN(0,0) + new BFN(0,0) );

		[Test] public void _1e4__1 () => Assert.AreEqual( expected:new BFN(10.001,3) , actual:new BFN(1,4) + 1 );
	}
	

	public class subtraction
	{
		[Test] public void _1e0__1e0 () => Assert.AreEqual( expected:new BFN(0,0) , actual:new BFN(1,0) - new BFN(1,0) );
		[Test] public void _1e0__2e0 () => Assert.AreEqual( expected:new BFN(-1,0) , actual:new BFN(1,0) - new BFN(2,0) );
		[Test] public void _3e10__4e3 () => Assert.AreEqual( expected:new BFN(29.999996,9) , actual:new BFN(3,10) - new BFN(4,3) );
		[Test] public void _1e100__1e70 () => Assert.AreEqual( expected:new BFN(1,100) , actual:new BFN(1,100) - new BFN(1,70) );
		[Test] public void _1e10__0e10 () => Assert.AreEqual( expected:new BFN(1,10) , actual:new BFN(1,10) - new BFN(0,10) );
		[Test] public void _0e0__0e0 () => Assert.AreEqual( expected:new BFN(0,0) , actual:new BFN(0,0) - new BFN(0,0) );

		[Test] public void _1e4__1 () => Assert.AreEqual( expected:new BFN(9.999,3) , actual:new BFN(1,4) - 1 );
	}
	
	public class multiplication
	{
		[Test] public void _2e1__3e1 () => Assert.AreEqual( expected:new BFN(600,0).compressed , actual:new BFN(2,1) * new BFN(3,1) );
		[Test] public void _3e10__4e3 () => Assert.AreEqual( expected:new BFN(1.2,14).compressed , actual:new BFN(3,10) * new BFN(4,3) );
		[Test] public void _0e0__0e0 () => Assert.AreEqual( expected:new BFN(0,0) , actual:new BFN(0,0) * new BFN(0,0) );

		[Test] public void _1e4__1 () => Assert.AreEqual( expected:new BFN(2,4) , actual:new BFN(1,4) * 2 );
	}
	
	public class division
	{
		[Test] public void _1e3__2e1 () => Assert.AreEqual( expected:new BFN(50,0) , actual:new BFN(1,3) / new BFN(2,1) );
		[Test] public void _3e10__4e3 () => Assert.AreEqual( expected:new BFN(7.5,6) , actual:new BFN(3,10) / new BFN(4,3) );

		[Test] public void _1e3__2 () => Assert.AreEqual( expected:new BFN(500,0) , actual:new BFN(1,3) / 2 );
		[Test] public void _1e4__1 () => Assert.AreEqual( expected:new BFN(5,3) , actual:new BFN(1,4) / 2 );
	}

}
