import 'package:flutter/material.dart';
import 'package:flutter_frontend_crud/providers/cliente_provider.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';

class ClientesListScreen extends StatelessWidget {
  const ClientesListScreen({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return SafeArea(
      child: Scaffold(
        appBar: AppBar(
          title: const Text("Clientes Lista"),
        ),
        body: const Center(
          child: Column(
            children:  [
                Text("test"),
                ClientesListView(),
                ],
          ),
        ),
      ),
    );
  }
}

//class _ClientesListScreenState extends State<ClientesListScreen> {
//final List<String> _items = ["1", "2", "3", "4", "11", "22", "33", "55"];

//ojo omportante
class ClientesListView extends ConsumerWidget {
  const ClientesListView({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    final listClientes = ref.watch(clienteList);
    return listClientes.when(
      data: (clientes) {
        return Expanded(
          child: ListView.builder(
              itemCount: clientes.clientes?.length ?? 0,
              itemBuilder: (BuildContext context, int index) {
                return Padding(
                  padding: const EdgeInsets.all(8.0),
                  child: Card(
                    child: ListTile(
                        title:
                            Text(clientes.clientes?[index].nombreCliente ?? ''),
                        subtitle: Column(
                          crossAxisAlignment: CrossAxisAlignment.start,
                          children: [
                            Text(clientes.clientes?[index].apellidoCliente ??
                                ''),
                            Text(
                                clientes.clientes?[index].telefono.toString() ??
                                    ''),
                          ],
                        )),
                  ),
                );
              }),
        );
      },
      loading: () => const Center(
        child: CircularProgressIndicator(),
      ),
      error: (err, stack) => Text('Error $err'),
    );
  }
}




/*
  //POSIBLE PARA ELIMINAR
      appBar: AppBar(
        title: const Text("Listado de Clientes"),
      ),
      body: Column(
        children: [
          const Text("Detalles Cliente"),
          Expanded(
            child: ListView.builder(
              itemCount: _items.length,
              itemBuilder: (BuildContext context, int idx) {
                return Card(
                  color: Colors.greenAccent,
                  elevation: 2.0,
                  child: Padding(
                    padding: const EdgeInsets.all(8.0),
                    child: ListTile(
                      title: Text(_items[idx]),
                      trailing: const Icon(Icons.ac_unit_sharp),
                    ),
                  ),
                );
              },
            ),
          )
        ],
      ),
    );
  }
}
*/