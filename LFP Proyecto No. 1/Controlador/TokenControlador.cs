﻿using LFP_Proyecto_No._1.Modelo;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace LFP_Proyecto_No._1.Controlador
{
    class TokenControlador
    {
        private readonly static TokenControlador instancia = new TokenControlador();
        private ArrayList arrayListTokens = new ArrayList();
        private ArrayList arrayListErrors = new ArrayList();
        private int idToken = 1;
        private int idTokenError = 1;
        private string nombreGrafica = "";
        private TokenControlador()
        {
        }

        public static TokenControlador Instancia
        {
            get
            {
                return instancia;
            }
        }

        public void agregarToken(int fila, int columna, string lexema, string descripcion)
        {
            Token token = new Token(idToken, lexema, descripcion, columna, fila);
            arrayListTokens.Add(token);
            idToken++;
            
        }

        public void agregarError(int fila, int columna,string lexema, string descripcion)
        {
            Token token = new Token(idTokenError, lexema, descripcion, columna, fila);
            arrayListErrors.Add(token);
            idTokenError++;
        }

        public ArrayList getArrayListTokens()
        {
            return arrayListTokens;
        }

        public ArrayList getArrayListErrors()
        {
            return arrayListErrors;
        }
        public void clearListaTokens()
        {
            arrayListTokens.Clear();
        }

        public void clearListaTokensError()
        {
            arrayListErrors.Clear();
        }

        public string getNombreGrafica()
        {
            for (int i = 0; i < arrayListTokens.Count; i++)
            {
                Token tok = (Token)arrayListTokens[i];
                if ((tok.Lexema.Equals("Nombre") && ((Token)arrayListTokens[i - 1]).Lexema.Equals("{") && ((Token)arrayListTokens[i - 2]).Lexema.Equals(":")
                    && ((Token)arrayListTokens[i - 3]).Lexema.ToLower().Equals("grafica"))
                    ||
                    (tok.Lexema.Equals("Nombre") && ((Token)arrayListTokens[i - 1]).Lexema.Equals("}")))
                {
                    nombreGrafica = ((Token)arrayListTokens[i + 2]).Lexema;
                }
            }
            nombreGrafica = nombreGrafica.Replace("\"", "");

            return this.nombreGrafica;
        }

        public void ImprimirTokens(string name)
        {
            string cadena = "";
            string contenido = "";

            for (int i = 0; i < arrayListTokens.Count; i++)
            {
                Token tok = (Token)arrayListTokens[i];

                contenido = "<tr>\n" +
                    "     <th scope=\"row\">" + (i).ToString() + "</th>\n" +
                    "     <td>" + tok.Fila+ "</td>\n" +
                    "     <td>" + tok.Lexema + "</td>\n" +
                    "     <td>" + tok.Descripcion + "</td>\n" +
                    "</tr>";
                cadena = cadena + contenido;

            }
            string cadena2 = "<th scope =\"col\">No</th>\n" +
            "          <th scope=\"col\">Fila</th>\n" +
            "          <th scope=\"col\">Lexema</th>\n" +
            "          <th scope=\"col\">Token</th>\n";
            armarHTML(cadena, cadena2, "Tokens " + name);

        }

        public void ImprimirErrores(string name)
        {
            string cadena = "";
            string contenido = "";

            for (int i = 0; i < arrayListErrors.Count; i++)
            {
                Token tok = (Token)arrayListErrors[i];

                contenido = "<tr>\n" +
                    "     <th scope=\"row\">" + (i).ToString() + "</th>\n" +
                    "     <td>" + tok.Fila + "</td>\n" +
                    "     <td>" + tok.Lexema + "</td>\n" +
                    "     <td>" + tok.Descripcion + "</td>\n" +
                    "</tr>";
                cadena = cadena + contenido;

            }
            string cadena2 = "<th scope =\"col\">No</th>\n" +
            "          <th scope=\"col\">Fila</th>\n" +
            "          <th scope=\"col\">Lexema</th>\n" +
            "          <th scope=\"col\">Token</th>\n";
            armarHTML(cadena, cadena2, "Errores " + name);

        }



        public void armarHTML(string cadena, string cadena2, string titulo)
        {

            string head = "<!DOCTYPE html>\n" +
            "<html>\n" +
            "<head>\n" +
            "    <meta charset='utf-8'>\n" +
            "    <meta http-equiv='X-UA-Compatible' content='IE=edge'>\n" +
            "    <title> Repote " + titulo + "</title>\n" +
            "    <meta name='viewport' content='width=device-width, initial-scale=1'>\n" +
            "    <link rel='stylesheet' type='text/css' media='screen' href='main.css'>\n" +
            "    <script src='main.js'></script>\n" +
            "    <link rel=\"stylesheet\" href=\"https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css\">\n" +
            "    <link rel=\"stylesheet\" href=\"https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css\">\n" +
            "</head>" +
            "<body>\n" +
            "  <nav class=\"navbar navbar-light bg-light\">\n" +
            "    <span class=\"navbar-brand mb-0 h1\">Lenguajes formales</span>\n" +
            "  </nav>";

            string body1 = "<div class=\"container\">\n" +
          "    <div class=\"jumbotron jumbotron-fluid\">\n" +
          "      <div class=\"container\">\n" +
          "        <h1 class=\"display-4\">" + titulo + "</h1>\n" +
          "        <p class=\"lead\">Listado de " + titulo + " detectados por el analizador</p>\n" +
          "      </div>\n" +
          "    </div>\n" +
          "    <div class=\"row\">\n" +
          "    <table id=\"data\"  cellspacing=\"0\" style=\"width: 100 %\" class=\"table table-striped table-bordered table-sm\">\n" +
          "      <thead class=\"thead-dark\">\n" +
          "        <tr>\n" +
                    cadena2 +
          "        </tr>\n" +
          "      </thead>" +
          "<tbody>";


            string body2 = "</tbody>\n" +
           "    </table>\n" +
           "</div>\n" +
           "  </div>";

            string script =
                "  <script src=\"https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/js/bootstrap.min.js\" ></script>\n" +
                "  <script src=\"https://cdnjs.cloudflare.com/ajax/libs/jquery/3.2.1/jquery.min.js\"></script>\n" +
                "  <script src=\"https://cdn.datatables.net/1.10.16/js/jquery.dataTables.min.js\"></script>\n" +
                "  <script src=\"https://cdn.datatables.net/1.10.16/js/dataTables.bootstrap4.min.js\" ></script>\n" +
                "<script>" +
                "$(document).ready(function () { " +
                 "$('#data').DataTable(" +

                 "{ \"aLengthMenu\" " + ":" + " [[5, 10, 25, -1], [5, 10, 25, \"All\"]], \"iDisplayLength\" : 5" +
                 "}" +
                 ");" +
                 "}" +
                 "); " +
               "</script>";

            string html;

            html = head + body1 + cadena + body2 +
            script +
            "</body>" +
            "</html>";


            /*creando archivo html*/
            File.WriteAllText("Reporte de " + titulo + ".html", html);
            System.Diagnostics.Process.Start("Reporte de " + titulo + ".html");

        }



    }
}
