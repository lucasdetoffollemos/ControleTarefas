--insert into TBTarefa 
--	(
--		[Prioridade], 
--		[Titulo], 
--		[DataCriacao],
--		[DataConclusao],
--		[Percentual]
--	) 
--	values 
--	(
--		'Alta', 
--		'Cuidar de idosos',
--		'06/17/2021',
--		'06/18/2021',
--		0
--	)

	--SELECT SCOPE_IDENTITY();


--update TBTarefa 
--	set	
--		[Nome] = 'Gabriel Batista', 
--		[DataNascimento]='05/05/1998', 
--		[Salario] = 5000
--	where 
--		[Id] = 16

--Delete from TBTarefa 
--	where 
--		[Id] = 1

select [Id], [Prioridade], [Titulo], [DataCriacao],[DataConclusao], [Percentual] from TBTarefa 

--select [Id], [Prioridade], [Titulo], [DataCriacao],[DataConclusao], [Percentual] from TBTarefa 
--	where 
--		[Id] = 1