--insert into TBCompromisso 
--	(
--		[Assunto], 
--		[Local], 
--		[DataCompromisso],
--		[HoraInicio],
--		[HoraTermino],
--		[Id_Contato]
--	) 
--	values 
--	(
--		'Lavar',
--		'Lages',
--		'06/01/2000',
--		'14:30',
--		'15:30',
--		1
--	)

--	SELECT SCOPE_IDENTITY();

--insert into TBCompromisso
--	(
--		[Nome], 
--		[DataNascimento], 
--		[Salario]
--	) 
--	values 
--	(
--		'Gabriel Batista',
--		'05/05/1998',
--		5000
--	)
--update TBFuncionario 
--	set	
--		[Nome] = 'Gabriel Batista', 
--		[DataNascimento]='05/05/1998', 
--		[Salario] = 5000
--	where 
--		[Id] = 16

--Delete from TBFuncionario 
--	where 
--		[Id] = 1

select [Id], [Assunto], [Local], [DataCompromisso], [HoraInicio], [HoraTermino], [Id_Contato] from TBCompromisso

--select [Id], [Nome], [DataNascimento], [Salario] from TBFuncionario 
--	where 
--		[Id] = 3