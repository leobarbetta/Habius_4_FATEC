<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Erro.aspx.cs" Inherits="Pages_Error_Erro" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link href="../../../css/bootstrap.min.css" rel="stylesheet" />
    <link href="../../../css/style.css" rel="stylesheet" />
    <style>
        body {
            background-color: #003563;
        }
    </style>
</head>

<body>
    <form id="form1" runat="server">


        <div class="container-fluid">

            <div class="row">
                <img class="img-responsive" style="height: 30%; width: 40%; display: block; margin-left: auto; margin-right: auto" src="../../Images/HabiusLogo.png" />
            </div>
            <div class="row">
                <h1 class="text-center" style="color: white; font-size: 80px"><strong>Ocorreu um erro!</strong></h1>
            </div>
            <div class="row">
                <div class="col-md-4 col-md-offset-4">
                    <asp:LinkButton ID="btnVoltar" class="btn btn-link" OnClick="btnVoltar_Click" Style="color: white; font-size: 40px; width: 100%" runat="server">Voltar para Home Page</asp:LinkButton>
                </div>
            </div>



        </div>
    </form>
</body>
</html>
