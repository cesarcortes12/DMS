class ClienteResponse {
  List<Cliente>? clientes;

  ClienteResponse({this.clientes});

  ClienteResponse.fromJson(Map<String, dynamic> json) {
    if (json['clientes'] != null) {
      clientes = List<Cliente>.from(
        json['clientes'].map((cliente) => Cliente.fromJson(cliente)),
      );
    }
  }

  Map<String, dynamic> toJson() {
    final Map<String, dynamic> data = {};
    if (clientes != null) {
      data['clientes'] = clientes?.map((cliente) => cliente.toJson()).toList();
    }
    return data;
  }
}

class Cliente {
  int? idCliente;
  String? nombreCliente;
  String? apellidoCliente;
  String? telefono;

  Cliente({this.idCliente, this.nombreCliente, this.apellidoCliente, this.telefono});

  Cliente.fromJson(Map<String, dynamic> json) {
    idCliente = json['idCliente'];
    nombreCliente = json['nombreCliente'];
    apellidoCliente = json['apellidoCliente'];
    telefono = json['telefono'];
  }

  Map<String, dynamic> toJson() {
    final Map<String, dynamic> data = {};
    data['idCliente'] = idCliente;
    data['nombreCliente'] = nombreCliente;
    data['apellidoCliente'] = apellidoCliente;
    data['telefono'] = telefono;
    return data;
  }
}
