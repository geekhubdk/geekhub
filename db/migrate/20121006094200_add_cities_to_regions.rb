# encoding: utf-8
class AddCitiesToRegions < ActiveRecord::Migration
  def up
    change_table :cities do |t|
      t.references :region
    end

    nordjylland = Region.create({name: "Nordjylland"}).id
    midtjylland = Region.create({name: "Midtjylland"}).id
    syddanmark  = Region.create({name: "Syddanmark"}).id
    sjælland    = Region.create({name: "Sjælland"}).id
    hovedstaden = Region.create({name: "Hovedstaden"}).id

    City.find_by_name("Hellerup").update_column :region_id,hovedstaden
    City.find_by_name("Horsens").update_column :region_id,midtjylland
    City.find_by_name("København").update_column :region_id,hovedstaden
    City.find_by_name("Aarhus").update_column :region_id,midtjylland
    City.find_by_name("Odense").update_column :region_id,syddanmark
    #Malmö
    #Online
    City.find_by_name("Billund").update_column :region_id,syddanmark
    City.find_by_name("Aalborg").update_column :region_id,nordjylland
    City.find_by_name("Taastrup").update_column :region_id,hovedstaden
    City.find_by_name("Høje Taastrup").update_column :region_id,hovedstaden
    City.find_by_name("Frederiksberg").update_column :region_id,hovedstaden
    City.find_by_name("Esbjerg").update_column :region_id,syddanmark
    City.find_by_name("Bjerringbro").update_column :region_id,midtjylland
    City.find_by_name("Vamdrup").update_column :region_id,syddanmark
    #Lund (Sverige)
    City.find_by_name("Vedbæk").update_column :region_id,hovedstaden
    City.find_by_name("Brabrand").update_column :region_id,midtjylland
  end
end
