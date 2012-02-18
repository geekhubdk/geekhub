# http://jeffgardner.org/2011/08/04/rails-string-to-boolean-method/
class String
  def to_bool
    return true if self == true || self =~ (/(true|t|yes|y|1)$/i)
    return false
    # return false if self == false || self.blank? || self =~ (/(false|f|no|n|0)$/i)
    # raise ArgumentError.new("invalid value for Boolean: \"#{self}\"")
  end
end