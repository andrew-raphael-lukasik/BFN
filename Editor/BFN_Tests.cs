// src* = https://github.com/andrew-raphael-lukasik/BFN
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
public class BFN_Tests
{

	[Test] public void equality_between_2_defaults () => Assert.AreEqual( expected: default(BFN) , actual: default(BFN) );
	[Test] public void equality_between_default_and_simplified_default () => Assert.AreEqual( expected: default(BFN) , actual: default(BFN).simplified );

    [Test] public void equality_between_value_and_its_simplified_value_1 () => Assert.AreEqual( expected:new BFN{ number=1 } , actual:new BFN{ number=1 }.simplified );
    [Test] public void equality_between_value_and_its_simplified_value_2 () => Assert.AreEqual( expected:new BFN{ number=1 , exponent=3 } , actual:new BFN{ number=1000 }.simplified );

    [Test] public void simplification_positive_exponent_1 () => Assert.AreEqual( expected:new BFN{ number=12.345678910111213141 , exponent=18 } , actual:new BFN{ number=12345678910111213141 }.simplified );
    [Test] public void simplification_positive_exponent_2 () => Assert.AreEqual( expected:new BFN{ number=9.999999999999999999 , exponent=18 } , actual:new BFN{ number=9999999999999999999 }.simplified );
    [Test] public void simplification_negative_exponent_1 () => Assert.AreEqual( expected:new BFN{ number=0.01 , exponent=-3 } , actual:new BFN{ number=0.00001 }.simplified );
    
    [Test] public void addition_1 () => Assert.AreEqual( expected:new BFN{ number=3.0000004000 , exponent=10 } , actual:new BFN{ number=3 , exponent=10 } + new BFN{ number=4 , exponent=3 } );

}
