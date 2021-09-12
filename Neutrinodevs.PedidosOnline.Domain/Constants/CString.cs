namespace Neutrinodevs.PedidosOnline.Domain.Constants
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
    }
}
