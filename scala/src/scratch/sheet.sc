package scratch

object sheet {
	def abs(x: Double) = if (x < 0) -x else x //> abs: (x: Double)Double
	
	def sqrIter(guess: Double, x: Double): Double =
		if(isGoodEnough(guess, x)) guess
		else sqrIter(improve(guess, x), x)//> sqrIter: (guess: Double, x: Double)Double
		
		def isGoodEnough(guess: Double, x: Double) =
			//abs(guess * guess - x)< 0.001
			abs((guess * guess) / x - 1.0) < 0.001
                                                  //> isGoodEnough: (guess: Double, x: Double)Boolean
			
		def improve(guess: Double, x: Double) =
			(guess + x / guess) / 2   //> improve: (guess: Double, x: Double)Double
			
		def sqrt(x: Double) = sqrIter(1.0, x)
                                                  //> sqrt: (x: Double)Double
		
		sqrt(2)                           //> res0: Double = 1.4142156862745097
		sqrt(4)                           //> res1: Double = 2.000609756097561
		sqrt(1e-6)                        //> res2: Double = 0.0010000001533016628
		sqrt(1e60)                        //> res3: Double = 1.0000788456669446E30
		
		sqrt(0.001)                       //> res4: Double = 0.03162278245070105
		sqrt(0.1e-20)                     //> res5: Double = 3.1633394544890125E-11
		sqrt(1.0e20)                      //> res6: Double = 1.0000021484861237E10
		sqrt(1.0e50)                      //> res7: Double = 1.0000003807575104E25
		
}