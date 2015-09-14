begin tran
	select * into #b from brands where not exists ( select * from bikes where bikes.BrandID = Brands.BrandID) and BrandID >= 8
	delete brands where not exists ( select * from bikes where bikes.BrandID = Brands.BrandID) and BrandID >= 8

	Print 'Removing dupes:'
	delete from Brands
	where BrandID NOT IN (
		select min(brandID)
		from Brands
		group by BrandName
	)

--rollback
--commit

	select 'commit?'
	select 'before', * from #b		
	select 'after', * from brands 

	