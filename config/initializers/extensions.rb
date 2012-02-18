# http://jeffgardner.org/2011/08/04/rails-string-to-boolean-method/
class String
  def to_bool
    return true if self == true || self =~ (/(true|1)$/i)
    return false if self == false || self =~ (/(false|0)$/i)
    return nil
  end
end