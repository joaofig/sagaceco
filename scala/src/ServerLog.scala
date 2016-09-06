// Server log sample

object ServerLog {
  def main(args: Array[String]): Unit = {
    println("Hello, world!")
  }
}

class LogRecord {
  
}

class WeeklyLogModel(radius: Double) {
  
  def update(record: LogRecord) {
    
  }
  
  def isOutlier(record: LogRecord): Boolean = {
    false
  }
  
  def getValue(record: LogRecord): Double = {
    0.0
  }
}

class LinearRegressionModel {
  private var count: Int = 0
  private var sumX: Double = 0.0
  private var sumY: Double = 0.0
  private var sumXY: Double = 0.0
  private var sumXX: Double = 0.0
  private var sumYY: Double = 0.0
  private var alpha: Double = 0.0
  private var beta: Double = 0.0
   
  def update(x: Double, y: Double) {
    sumX += x
    sumY += y
    sumXY += x * y
    sumXX += x * x
    sumYY += y * y
    count += 1
    
    val meanX = sumX / count
    val meanY = sumY / count
    val meanXY = sumXY / count
    val meanXX = sumXX / count
    
    beta = (meanXY - meanX * meanY) / (meanXX - meanX * meanX)
    alpha = meanY - beta * meanX
  }
  
  def getValue(x: Double): Double = {
    alpha + beta * x
  }
  
  def isOutlier(radius: Double, x: Double, y: Double): Boolean = {
    val yHat = alpha + beta * x
    val studentizedResidue = math.abs(y - yHat) / math.sqrt( getResidualVariance );
    studentizedResidue > radius
  }
  
  // Private stuff
  
  private def getResidualSumOfSquares(): Double = {
    beta * beta * sumXX - 2 * beta * sumXY + 2 * alpha * beta * sumX + sumYY - 2 * alpha * sumY + alpha * alpha * count
  }
  
  private def getResidualVariance(): Double = {
    getResidualSumOfSquares / (count - 2)
  }
}
