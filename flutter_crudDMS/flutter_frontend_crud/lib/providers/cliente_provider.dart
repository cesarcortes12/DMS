import 'package:flutter_frontend_crud/models/cliente.dart';
import 'package:flutter_frontend_crud/repositories/cliente_repository.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';

final clienteRepositoryProvider =
    Provider<IClienteRepository>((ref) => ClienteRepository());

final clienteList = FutureProvider.autoDispose<ClienteResponse>((ref) async {
  final repository = ref.watch(clienteRepositoryProvider);
  return repository.fetchClientesList();
});
