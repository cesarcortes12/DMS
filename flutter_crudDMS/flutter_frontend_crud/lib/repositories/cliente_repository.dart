//import 'package:flutter/services.dart';
import 'package:flutter_frontend_crud/models/cliente.dart';
import 'dart:convert';
import 'package:http/http.dart' as http;

abstract class IClienteRepository {
  Future<ClienteResponse> fetchClientesList();
}

class ClienteRepository implements IClienteRepository {
  final _host = "https://192.168.80.1:7004/api/Clientes";
  final Map<String, String> _headers = {
    "Accept": "application/json",
    "content-type": "application/json",
  };
  @override
  Future<ClienteResponse> fetchClientesList() async {
    var getAllClientesUrl = '$_host' '/obtener-todos-clientes';
    print(getAllClientesUrl);
    var results =
        await http.get(Uri.parse(getAllClientesUrl), headers: _headers);

    final jsonObject = json.decode(results.body);

    var response = ClienteResponse.fromJson(jsonObject);

    return response;
  }
}
