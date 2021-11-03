﻿namespace Neutrinodevs.PedidosOnline.Domain.Constants
{
    public static class CString
    {
        public const string EMAIL_TEMPLATE = "<div style='background-color: #f8f5f1; color: #555; width: 80%; height:360px; margin: 0 auto; border-bottom: 5px solid #000; font-family: Arial, Helvetica, sans-serif;'>"
                                + "<div style = 'background-color: #000; height:70px; padding:3px;'>"
                                     + "<h2 style = 'color:#fff; text-align:center'>"
                                        + "Verificación de correo electrónico"
                                    + "<h2>"
                                + "</div>"
                                + "<section style = 'padding: 20px;'>"
                                    + "<p style='font-size:20px;line-height:1.9rem'>Código de verificación:</p>"
                                        + "<div style = 'font-size: 22px; border-radius: 25px; background-color:#fff;font-weight:bold;text-align:center;width:80%; margin: 0 auto;'>"
                                        + "<p style='padding:5px 5px 15px'>@code </p>"
                                    + "</ div >"
                                + "</section>"
                                + "</div>";

        public const string RECIBO_TEMPLATE = "<div style='background-color: #e2e2e2'>	<div style='background-color: white; color: #555; width: 80%; height:600px; margin: 0 auto; border-bottom: 5px solid #000; font-family: Arial, Helvetica, sans-serif;'>		<div style = 'background-color: #000; height:170px; padding:3px; text-align:center;'>			<img src='http://bduarte-001-site1.ftempurl.com/img/logo-la-pesca.png' width='170' height='170'/>			<h2 style = 'color:#000; text-align:center; margin-top:40px'>Gracias por usar nuestro servicio online.</h2>			<h3>Esperamos que disfrute su pedido.</h3>			<section style='padding-top: 15px; padding-left:70px; padding-right:70px;'>				<div style='float:left;'>					<h1 style='color:#000; text-align:left'>TOTAL</h1>					<hr>					<h4 style='color:#000; text-align:left'>SUBTOTAL</h4>					<h4 style='color:#000; text-align:left'>IVA</h4>					<h4 style='color:#000; text-align:left'>RECARGO DE ENTREGA</h4>				</div>				<div style='float:right;'>					<h1 style='color:#000; text-align:right'>$@total</h1>					<hr>					<h4 style='color:#000; text-align:right'>$@subtotal</h4>					<h4 style='color:#000; text-align:right'>$@iva</h4>					<h4 style='color:#000; text-align:right'>$0.00</h4>				</div>			</section>		</div>	</div></div>";
    }
}
