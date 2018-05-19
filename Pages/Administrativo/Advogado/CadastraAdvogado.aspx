<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Administrativo/Advogado/MasterAdvogado.master" AutoEventWireup="true" CodeFile="CadastraAdvogado.aspx.cs" Inherits="Pages_Administrativo_CadastraAdvogado" %>



<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../../../JavaScript/Mask.js"></script>
    <div runat="server" id="divmensagem">
        <asp:Label ID="lblMensagem" runat="server"></asp:Label>
    </div>
    <div id="main" class="container-fluid formCadastro">
        <asp:Panel runat="server" DefaultButton="btnCadastrar">
            <h1 class="text-center page-header text" style="color: rgba(77, 77, 77, 0.90)">CADASTRAR ADVOGADO</h1>

            <div class="row">
                <div class="col-md-10 col-md-offset-1">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <h3 class="panel-title"><span class="glyphicon glyphicon-user" style="padding-right: 5px" aria-hidden="true"></span>Dados Pessoais</h3>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-6">
                                    <div id="formNome" runat="server" class="form-group">
                                        <label class="control-label" for="txtNome">Nome Completo:<span style="color: red; padding-left: 5px">*</span></label>
                                        <asp:TextBox ID="txtNome" ClientIDMode="Static" class="form-control input-lg" runat="server" type="text" minlength="10" required oninvalid="setCustomValidity('Por favor, preencha seu nome.')" onchange="try{setCustomValidity('')}catch(e){}" />
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="control-label" for="txtEmail">Email:<span style="color: red; padding-left: 5px">*</span></label>
                                        <asp:TextBox ID="txtEmail" runat="server" ClientIDMode="Static" class="form-control input-lg" type="email" required pattern="[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}$" oninvalid="setCustomValidity('Por favor, insira um email válido.')" onchange="try{setCustomValidity('')}catch(e){}" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>RG:<span style="color: red; padding-left: 5px">*</span></label>
                                        <asp:TextBox ID="txtRg" ClientIDMode="Static" class="form-control input-lg" runat="server" placeholder="Somente números" MaxLength="12" required minlength="12" pattern="\d{2}\.\d{3}\.\d{3}-\d{1}" oninvalid="setCustomValidity('Por favor, insira um RG válido.')" onchange="try{setCustomValidity('')}catch(e){}" />
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label for="txtCPF">CPF:<span style="color: red; padding-left: 5px">*</span></label>
                                        <asp:TextBox ID="txtCPF" ClientIDMode="Static" class="form-control input-lg" runat="server" placeholder="Somente números" MaxLength="14" pattern="\d{3}\.\d{3}\.\d{3}-\d{2}" required oninvalid="setCustomValidity('Por favor, insira um CPF válido.')" onchange="try{setCustomValidity('')}catch(e){}" />
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Data de Nascimento:<span style="color: red; padding-left: 5px">*</span></label>
                                        <asp:TextBox ID="txtDataNascimento" ClientIDMode="Static" class="form-control input-lg" runat="server" placeholder="Somente números" pattern="[0-9]{2}\/[0-9]{2}\/[0-9]{4}$" required oninvalid="setCustomValidity('Por favor, preencha sua data de nascimento.')" onchange="try{setCustomValidity('')}catch(e){}" />
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Sexo:<span style="color: red; padding-left: 5px">*</span></label>
                                        <asp:DropDownList ID="ddlSexo" class="form-control input-lg" ClientIDMode="Static" runat="server">
                                            <asp:ListItem Value="0" Text="Selecione" />
                                            <asp:ListItem Value="M" Text="Masculino" />
                                            <asp:ListItem Value="F" Text="Feminino" />
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>OAB:<span style="color: red; padding-left: 5px">*</span></label>
                                        <asp:TextBox ID="txtOab" runat="server" ClientIDMode="Static" class="form-control input-lg" MaxLength="10" OnKeyPress="return validaTecla(this, event)" type="text" required oninvalid="setCustomValidity('Por favor, insira seu nº OAB.')" onchange="try{setCustomValidity('')}catch(e){}" />
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Estado Civil:<span style="color: red; padding-left: 5px">*</span></label>
                                        <asp:DropDownList ID="ddlEstadoCivil" class="form-control input-lg" ClientIDMode="Static" runat="server" />
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Telefone (+DDD):<span style="color: red; padding-left: 5px">*</span></label>
                                        <asp:TextBox ID="txtTelefone" ClientIDMode="Static" runat="server" class="form-control input-lg" placeholder="Somente números" required pattern="\([0-9]{2}\) [0-9]{4,6}-[0-9]{3,4}$" oninvalid="setCustomValidity('Por favor, insira seu telefone.')" onchange="try{setCustomValidity('')}catch(e){}" />
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Celular (+DDD):</label>
                                        <asp:TextBox ID="txtCelular" ClientIDMode="Static" class="form-control input-lg" runat="server" placeholder="Somente números" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-md-10 col-md-offset-1">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <h3 class="panel-title"><span class="glyphicon glyphicon-menu-hamburger" style="padding-right: 5px" aria-hidden="true"></span>Dados de Endereço</h3>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Endereço:<span style="color: red; padding-left: 5px">*</span></label>
                                        <asp:TextBox ID="txtEndereco" ClientIDMode="Static" runat="server" class="form-control input-lg" required minlength="10" oninvalid="setCustomValidity('Por favor, preencha seu endereço.')" onchange="try{setCustomValidity('')}catch(e){}" />
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Bairro:<span style="color: red; padding-left: 5px">*</span></label>
                                        <asp:TextBox ID="txtBairro" ClientIDMode="Static" class="form-control input-lg" runat="server" required minlength="4" oninvalid="setCustomValidity('Por favor, preencha seu bairro.')" onchange="try{setCustomValidity('')}catch(e){}" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Numero:<span style="color: red; padding-left: 5px">*</span></label>
                                        <asp:TextBox ID="txtNumero" ClientIDMode="Static" runat="server" class="form-control input-lg" required oninvalid="setCustomValidity('Por favor, insira o numero de sua residência.')" onchange="try{setCustomValidity('')}catch(e){}" />
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Complemento:</label>
                                        <asp:TextBox ID="txtComplemento" runat="server" class="form-control input-lg" />
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>CEP:<span style="color: red; padding-left: 5px">*</span></label>
                                        <asp:TextBox ID="txtCEP" runat="server" ClientIDMode="Static" class="form-control input-lg" placeholder="Somente números" MaxLength="9" OnKeyPress="Mask('#####-###', this)" required pattern="(^\d{5}-\d{3}|^\d{2}.\d{3}-\d{3}|\d{8})" oninvalid="setCustomValidity('Por favor, insira seu CEP.')" onchange="try{setCustomValidity('')}catch(e){}" />
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Estado:<span style="color: red; padding-left: 5px">*</span></label>
                                        <asp:DropDownList ID="ddlEstado" class="form-control input-lg" ClientIDMode="Static" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlEstado_SelectedIndexChanged" />
                                    </div>
                                </div>
                                <asp:UpdatePanel ID="update" UpdateMode="Conditional" runat="server">
                                    <ContentTemplate>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Cidade:<span style="color: red; padding-left: 5px">*</span></label>
                                                <asp:DropDownList ID="ddlCidade" class="form-control input-lg" ClientIDMode="Static" runat="server" />
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="ddlEstado" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>

                        </div>
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-md-10 col-md-offset-1">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <h3 class="panel-title"><span class="glyphicon glyphicon-lock" style="padding-right: 5px" aria-hidden="true"></span>Dados de Login</h3>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Usuário:<span style="color: red; padding-left: 5px">*</span></label>
                                        <asp:TextBox ID="txtLogin" class="form-control input-lg" ClientIDMode="Static" runat="server" required oninvalid="setCustomValidity('Por favor, insira seu nome de usuário.')" onchange="try{setCustomValidity('')}catch(e){}" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Senha:<span style="color: red; padding-left: 5px">*</span></label>
                                        <asp:TextBox ID="txtSenha" class="form-control input-lg" ClientIDMode="Static" runat="server" TextMode="Password" required oninvalid="setCustomValidity('Por favor, insira sua senha.')" onchange="try{setCustomValidity('')}catch(e){}" />
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Confirmar Senha:<span style="color: red; padding-left: 5px">*</span></label>
                                        <asp:TextBox ID="txtConfirmaSenha" class="form-control input-lg" ClientIDMode="Static" runat="server" TextMode="Password" oninput="validaSenha(this)" required oninvalid="setCustomValidity('Por favor, confirme sua senha.')" onchange="try{setCustomValidity('')}catch(e){}" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
          
            <div class="row">
                <div class="col-sm-2 col-sm-offset-4">
                    <asp:Button ID="btnCadastrar" runat="server" CssClass="btn btn-lg btn-block btn-success active" CausesValidation="true" Text="Cadastrar" OnClick="btnCadastrar_Click" />
                </div>
                <div class="col-sm-2">
                    <asp:Button ID="btnVoltar" runat="server" CausesValidation="false" CssClass="btn btn-block btn-lg btn-danger active" Text="Voltar" OnClick="btnVoltar_Click" />
                </div>
            </div>
        </asp:Panel>
    </div>






    <script type="text/javascript">
        function validaSenha(input) {
            if (input.value != document.getElementById('txtSenha').value) {
                input.setCustomValidity('Repita a senha corretamente');
            } else {
                input.setCustomValidity('');
            }
        }
    </script>


</asp:Content>

