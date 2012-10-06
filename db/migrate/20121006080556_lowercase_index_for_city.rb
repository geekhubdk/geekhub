class LowercaseIndexForCity < ActiveRecord::Migration
  def up
    execute <<-SQL
      CREATE INDEX lower_city_name ON cities(lower(name))
    SQL
  end

  def down
  end
end
