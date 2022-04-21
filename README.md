# ASP.NET CORE + API REST + Read XML + CFDI

Realizar un Web Service REST que reciba 
    RFC (texto)
    CFDI(XML) 
    
Realice las siguientes validaciones sobre el campo CFDI:
* El campo RFC corresponda al atributo RFC del nodo cfdi:Emisor
* Que el atributo versión del nodo cfdi:Comprobante tenga el valor “3.3”
* Que la suma de los atributos importe de los nodos cfdi:concepto sea igual al atributo SubTotal del nodo cfdi:comprobante
