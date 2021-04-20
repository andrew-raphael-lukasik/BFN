// src* = https://github.com/andrew-raphael-lukasik/BFN
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
public class BFN_Tests
{

	[Test] public void equality_between_2_defaults () => Assert.AreEqual( expected: default(BFN) , actual: default(BFN) );
	[Test] public void equality_between_default_and_simplified_default () => Assert.AreEqual( expected: default(BFN) , actual: default(BFN).compressed );

    [Test] public void equality_between_value_and_its_simplified_value_1 () => Assert.AreEqual( expected:new BFN(1,0) , actual:new BFN(1,0).compressed );
    [Test] public void equality_between_value_and_its_simplified_value_2 () => Assert.AreEqual( expected:new BFN(1,3) , actual:new BFN(1000,0).compressed );

    [Test] public void simplification_positive_exponent_1 () => Assert.AreEqual( expected:new BFN(12.345678910111213141,18) , actual:new BFN(12345678910111213141,0).compressed );
    [Test] public void simplification_positive_exponent_2 () => Assert.AreEqual( expected:new BFN(9.999999999999999999,18) , actual:new BFN(9999999999999999999,0).compressed );
    [Test] public void simplification_negative_exponent_1 () => Assert.AreEqual( expected:new BFN(0.01,-3) , actual:new BFN(0.00001,0).compressed );
    
    [Test] public void addition_1 () => Assert.AreEqual( expected:new BFN(12,3) , actual:new BFN(1,4) + new BFN(2,3) );
    [Test] public void addition_2 () => Assert.AreEqual( expected:new BFN(9.87654321,33) , actual:new BFN(9.87654321,33) + new BFN(1.23456789,14) );
    [Test] public void addition_3 () => Assert.AreEqual( expected:new BFN(30.000004,9) , actual:new BFN(3,10) + new BFN(4,3) );
    [Test] public void addition_4 () => Assert.AreEqual( expected:new BFN(1,14) , actual:new BFN(1,14) + new BFN(0,13) );
    [Test] public void addition_5 () => Assert.AreEqual( expected:new BFN(1,14) , actual:new BFN(1,14) + new BFN(0,-13) );
    [Test] public void addition_6 () => Assert.AreEqual( expected:new BFN(-1,-14) , actual:new BFN(-1,-14) + new BFN(0,-13) );

    [Test] public void subtraction_1 () => Assert.AreEqual( expected:new BFN(0,0) , actual:new BFN(1,0) - new BFN(1,0) );
    [Test] public void subtraction_2 () => Assert.AreEqual( expected:new BFN(-1,0) , actual:new BFN(1,0) - new BFN(2,0) );
    [Test] public void subtraction_3 () => Assert.AreEqual( expected:new BFN(29.999996,9) , actual:new BFN(3,10) - new BFN(4,3) );
    [Test] public void subtraction_4 () => Assert.AreEqual( expected:new BFN(1,100) , actual:new BFN(1,100) - new BFN(1,70) );
    [Test] public void subtraction_5 () => Assert.AreEqual( expected:new BFN(1,10) , actual:new BFN(1,10) - new BFN(0,10) );
    
    [Test] public void multiplication_1 () => Assert.AreEqual( expected:new BFN(600,0).compressed , actual:new BFN(2,1) * new BFN(3,1) );
    [Test] public void multiplication_2 () => Assert.AreEqual( expected:new BFN(1.2,14).compressed , actual:new BFN(3,10) * new BFN(4,3) );
    
    [Test] public void division_1 () => Assert.AreEqual( expected:new BFN(500,0) , actual:new BFN(1,3) / 2 );
    [Test] public void division_2 () => Assert.AreEqual( expected:new BFN(50,0) , actual:new BFN(1,3) / new BFN(2,1) );
    [Test] public void division_3 () => Assert.AreEqual( expected:new BFN(7.5,6) , actual:new BFN(3,10) / new BFN(4,3) );

}
