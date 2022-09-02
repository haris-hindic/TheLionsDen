import 'dart:convert';

import 'package:the_lions_den_mobile/model/user/user_response.dart';
import 'package:the_lions_den_mobile/providers/base_provider.dart';

class UserProvider extends BaseProvider<UserResponse> {
  UserProvider() : super("User");

  @override
  UserResponse fromJson(data) {
    return UserResponse.fromJson(data);
  }

  Future<UserResponse> login() async {
    var url = Uri.parse("$fullUrl/login");

    Map<String, String> headers = createHeaders();

    var response = await http!.get(url, headers: headers);

    if (isValidResponseCode(response)) {
      var data = jsonDecode(response.body);
      return fromJson(data);
    } else {
      throw Exception("An error occured!");
    }
  }

  Future<UserResponse?> register(dynamic request) async {
    var url = Uri.parse("$fullUrl/register");

    Map<String, String> headers = {"Content-Type": "application/json"};
    var jsonRequest = jsonEncode(request);
    var response = await http!.post(url, headers: headers, body: jsonRequest);

    if (response.statusCode == 400) {
      var body = jsonDecode(response.body);
      var errMsg = body['errors'];
      throw Exception(errMsg.toString().replaceAll(RegExp(r'[\[\]{}]'), ''));
    }

    if (isValidResponseCode(response)) {
      var data = jsonDecode(response.body);
      return fromJson(data);
    } else {
      return null;
    }
  }

  Future<UserResponse?> customerUpdate(int id, dynamic request) async {
    var url = Uri.parse("$fullUrl/customer/$id");

    Map<String, String> headers = createHeaders();
    var jsonRequest = jsonEncode(request);
    var response = await http!.put(url, headers: headers, body: jsonRequest);

    if (response.statusCode == 400) {
      var body = jsonDecode(response.body);
      var errMsg = body['errors'];
      throw Exception(errMsg.toString().replaceAll(RegExp(r'[\[\]{}]'), ''));
    }

    if (isValidResponseCode(response)) {
      var data = jsonDecode(response.body);
      return fromJson(data);
    } else {
      return null;
    }
  }
}
