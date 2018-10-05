<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="iniciarsesion.aspx.cs" Inherits="Presentacion.iniciarsesion" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<!--<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />-->
	<title>Course</title>
	<meta charset="utf-8" />
	<meta http-equiv="X-UA-Compatible" content="IE=edge" />
	<meta name="description" content="Course Project" />
	<meta name="viewport" content="width=device-width, initial-scale=1" />
	<link rel="stylesheet" type="text/css" href="styles/bootstrap4/bootstrap.min.css">
	<link href="plugins/fontawesome-free-5.0.1/css/fontawesome-all.css" rel="stylesheet" type="text/css">
	<link rel="stylesheet" type="text/css" href="styles/main_styles.css">
	<link rel="stylesheet" type="text/css" href="styles/responsive.css">
	<!--<link rel="stylesheet" type="text/css" href="<%= ResolveUrl("~/styles/bootstrap4/bootstrap.min.css") %>" />
	<link href="<%= ResolveUrl("~/plugins/fontawesome-free-5.0.1/css/fontawesome-all.css") %>" rel="stylesheet" type="text/css" />
	<link rel="stylesheet" type="text/css" href="<%= ResolveUrl("~/styles/main_styles.css") %>" />
	<link rel="stylesheet" type="text/css" href="<%= ResolveUrl("~/styles/responsive.css") %>" />-->
</head>
<body>
	<form id="form1" runat="server">
		<!--<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>-->
		<div class="super_container">
			<div class="register">
				<div class="container-fluid">
					<div class="row row-eq-height">
						<div class="col-lg-6 nopadding">
							<div class="form-control" style="background-color: yellow">
								<asp:TextBox ID="txtUsuario" placeholder="Usuario" CssClass="form-control" runat="server"></asp:TextBox>
								<asp:RequiredFieldValidator ID="reqUsuario" runat="server" ControlToValidate="txtUsuario" Display="Dynamic" CssClass="fa fa-anchor btn-danger" style="font-size: 10px" SetFocusOnError="True" />
								<asp:TextBox ID="txtContrasena" placeholder="Contraseña" CssClass=" form-control "  runat="server"></asp:TextBox>
								<asp:RequiredFieldValidator ID="reqContrasena" runat="server" ControlToValidate="txtContrasena" Display="Dynamic" CssClass="fa fa-anchor btn-danger" style="font-size: 10px" SetFocusOnError="true" />
								<asp:Button ID="btnSesion" CssClass="btn btn-block btn-primary" runat="server" OnClick="btnSesion_Click" />
							</div>
						</div>
						<div class="col-lg-6 nopadding">
							<div class="form-control">
								<asp:PlaceHolder runat="server" ID="phAlert" />
							</div>
						</div>
					</div>
				</div>

			</div>

		</div>
		<script src="<%= ResolveUrl("~/js/jquery-3.2.1.min.js") %>"></script>
		<script src="<%= ResolveUrl("~/styles/bootstrap4/popper.js") %>"></script>
		<script src="<%= ResolveUrl("~/styles/bootstrap4/bootstrap.min.js") %>"></script>
		<script src="<%= ResolveUrl("~/js/custom.js") %>"></script>
	</form>
</body>
</html>
