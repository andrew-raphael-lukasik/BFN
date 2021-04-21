// src* = https://github.com/andrew-raphael-lukasik/BFN
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
public class BFN_Tests
{

	[Test] public void equal__default_default () => Assert.AreEqual( expected: default(BFN) , actual: default(BFN) );
	[Test] public void equal__1e3_1000e0 () => Assert.AreEqual( expected:new BFN(1,3) , actual:new BFN(1000,0) );

	[Test] public void compress__positive_exponent_1 () => Assert.AreEqual( expected:new BFN(12.345678910111213141,18) , actual:new BFN(12345678910111213141,0).compressed );
	[Test] public void compress__positive_exponent_2 () => Assert.AreEqual( expected:new BFN(9.999999999999999999,18) , actual:new BFN(9999999999999999999,0).compressed );
	[Test] public void compress__negative_exponent_1 () => Assert.AreEqual( expected:new BFN(0.01,-3) , actual:new BFN(0.00001,0).compressed );
	[Test] public void compress__0e0 () => Assert.AreEqual( expected:new BFN(0,0) , actual:new BFN(0,0).compressed );
	[Test] public void compress__123e123 () => Assert.AreEqual( expected:new BFN(123,123) , actual:new BFN(123,123).compressed );
	[Test] public void compress__default () => Assert.AreEqual( expected: default(BFN) , actual: default(BFN).compressed );
	[Test] public void compress__1e0 () => Assert.AreEqual( expected:new BFN(1,0) , actual:new BFN(1,0).compressed );
	
	[Test] public void addition__1e4_2e3 () => Assert.AreEqual( expected:new BFN(12,3) , actual:new BFN(1,4) + new BFN(2,3) );
	[Test] public void addition__9dot87654321e33_1dot23456789e14 () => Assert.AreEqual( expected:new BFN(9.87654321,33) , actual:new BFN(9.87654321,33) + new BFN(1.23456789,14) );
	[Test] public void addition__3e10_4e3 () => Assert.AreEqual( expected:new BFN(30.000004,9) , actual:new BFN(3,10) + new BFN(4,3) );
	[Test] public void addition__1e14_0e13 () => Assert.AreEqual( expected:new BFN(1,14) , actual:new BFN(1,14) + new BFN(0,13) );
	[Test] public void addition__1e14_0en13 () => Assert.AreEqual( expected:new BFN(1,14) , actual:new BFN(1,14) + new BFN(0,-13) );
	[Test] public void addition__n1en14_0en13 () => Assert.AreEqual( expected:new BFN(-1,-14) , actual:new BFN(-1,-14) + new BFN(0,-13) );
	[Test] public void addition__0e0_0e0 () => Assert.AreEqual( expected:new BFN(0,0) , actual:new BFN(0,0) + new BFN(0,0) );

	[Test] public void subtract__1e0_1e0 () => Assert.AreEqual( expected:new BFN(0,0) , actual:new BFN(1,0) - new BFN(1,0) );
	[Test] public void subtract__1e0_2e0 () => Assert.AreEqual( expected:new BFN(-1,0) , actual:new BFN(1,0) - new BFN(2,0) );
	[Test] public void subtract__3e10_4e3 () => Assert.AreEqual( expected:new BFN(29.999996,9) , actual:new BFN(3,10) - new BFN(4,3) );
	[Test] public void subtract__1e100_1e70 () => Assert.AreEqual( expected:new BFN(1,100) , actual:new BFN(1,100) - new BFN(1,70) );
	[Test] public void subtract__1e10_0e10 () => Assert.AreEqual( expected:new BFN(1,10) , actual:new BFN(1,10) - new BFN(0,10) );
	[Test] public void subtract__0e0_0e0 () => Assert.AreEqual( expected:new BFN(0,0) , actual:new BFN(0,0) - new BFN(0,0) );
	
	[Test] public void multiply__2e1_3e1 () => Assert.AreEqual( expected:new BFN(600,0).compressed , actual:new BFN(2,1) * new BFN(3,1) );
	[Test] public void multiply__3e10_4e3 () => Assert.AreEqual( expected:new BFN(1.2,14).compressed , actual:new BFN(3,10) * new BFN(4,3) );
	[Test] public void multiply__0e0_0e0 () => Assert.AreEqual( expected:new BFN(0,0) , actual:new BFN(0,0) * new BFN(0,0) );
	
	[Test] public void divide__1e3_2 () => Assert.AreEqual( expected:new BFN(500,0) , actual:new BFN(1,3) / 2 );
	[Test] public void divide__1e3_2e1 () => Assert.AreEqual( expected:new BFN(50,0) , actual:new BFN(1,3) / new BFN(2,1) );
	[Test] public void divide__3e10_4e3 () => Assert.AreEqual( expected:new BFN(7.5,6) , actual:new BFN(3,10) / new BFN(4,3) );

}
