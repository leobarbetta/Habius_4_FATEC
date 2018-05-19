USE HABIUS;

-- > INSERT DOS NIVEIS DE USUARIO DO SISTEMA
INSERT INTO NIV_NIVEL(NIV_CODIGO, NIV_DESCRICAO) VALUE(1,'Proprietario'), (2,'Advogado'), (3,'Cliente Fisico'), (4,'Cliente Juridico');

--  INSERT DOS ESTADOS CIVIS
INSERT INTO ECI_ESTADOCIVIL(ECI_CODIGO, ECI_DESCRICAO) VALUE(1,'solteiro(a)'),(2,'casado(a)'),(3,'viuvo(a)'),(4,'divorciado(a)');

-- > INSERT DE USUARIO PADRÃO COM LOGIN = 'ADMIN' E SENHA 'ADMIN' OBS: EM MINUSCULA
INSERT INTO CON_CONTATO(CON_CODIGO, CON_NOME, CON_CELULAR, CON_EMAIL) VALUE ('1','Administrador', '12 98144-1991', 'admin@admin.com');
INSERT INTO END_ENDERECO(END_CODIGO, CID_CODIGO, END_LOGRADOURO, END_NUMERO, END_COMPLEMENTO, END_BAIRRO, END_CEP) VALUE('1', '79', 'Domingos Rodrigues Alves', '352', '202', 'Centro', '12500-000');
INSERT INTO PES_PESSOA(PES_CODIGO, CON_CODIGO, NIV_CODIGO, ECI_CODIGO, END_CODIGO, PES_LOGIN, PES_SENHA, PES_DATACADASTRO, PES_CPF, PES_RG, PES_DATANASCIMENTO, PES_OAB, PES_SEXO) 
VALUE(	'1', '1', '2', '3', '1', 'admin', '7523c62abdb7628c5a9dad8f97d8d8c5c040ede36535e531a8a3748b6cae7e00', '2015-09-09', '123.456.789-02', '73.627.463-7', '1995-04-10', '3274897328', 'M');
UPDATE CON_CONTATO C SET C.PES_CODIGO = 1 WHERE C.CON_CODIGO = 1;

-- NORMALIZAÇÃO DOS ITENS DOS PROCESSOS
INSERT INTO VAR_VARA(VAR_CODIGO, VAR_VARA) value
(1,'Vara única'), (2,'1º Vara'), (3,'2º Vara'), (4,'3º Vara'),
(5,'4º Vara'), (6,'5º Vara'), (7,'6º Vara'), (8,'7º Vara'),
(9,'8º Vara'), (10,'9º Vara'), (11,'10º Vara'), (12,'11º Vara'),
(13,'12º Vara'), (14,'13º Vara'), (15,'14º Vara');

INSERT INTO CLA_CLASSE(CLA_CODIGO, CLA_CLASSE) value
(1,'Trabalhista'), (2,'Cível'), (3,'Criminal'), (4,'Previdenciário'),
(5,'Tributário'), (6,'Empresarial');

INSERT INTO MOV_MOVIMENTACAO(MOV_CODIGO, MOV_MOVIMENTACAO) VALUE
(1,'Ajuizamento de petição'), (2,'Autos no ofício'), (3,'Autos concluso'), (4,'Vista ao MP'),
(5,'Carga ao advogado'), (6,'Tribunal'), (7,'Finalizado');

INSERT INTO TRI_TRIBUNAL(TRI_CODIGO, TRI_TRIBUNAL) VALUE
(1,'TJ'), (2,'TRF'), (3,'TRT'), (4,'STJ'),
(5,'STF'), (6,'TST');

INSERT INTO POS_POSICAOCLIENTE(POS_CODIGO, POS_POSICAO) VALUE
(1,'Autor'), (2,'Réu'), (3,'Terceiro');

INSERT INTO TIP_TIPODESPESA(TIP_CODIGO, TIP_TIPODESPESA, TIP_CATEGORIA) VALUE
(1,'Agua', 1), (2,'Luz', 1), (3,'Internet', 1), (4,'Aluguel', 1), 
(5,'Opção 1', 2), (6,'Opção 2', 2), (7,'Opção 3', 2), (8,'Opção 3', 2), (9,'Outros', 0);

INSERT INTO SEV_SERVICO(SEV_CODIGO, SEV_SERVICO) VALUE
(1,'Processo'), (2,'Contrato'), (3,'Petição'), (4,'Outros');