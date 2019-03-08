;#DropDB MyCompanyInvoicesEasyTest

#Application MyCompanyInvoices.Win
#Application MyCompanyInvoices.Web

*FillForm
 User Name = Admin
*Action Log In

#IfDef MyCompanyInvoices.Win

*Action About...
 
*Action OK

#EndIf

#IfDef MyCompanyInvoices.Web
*Action Navigation(My Details)
#EndIf
