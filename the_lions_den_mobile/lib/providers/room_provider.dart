import 'dart:convert';
import 'dart:io';

import 'package:flutter/cupertino.dart';
import 'package:http/io_client.dart';
import 'package:the_lions_den_mobile/model/room/room_response.dart';
import 'package:the_lions_den_mobile/providers/base_provider.dart';

class RoomProvider extends BaseProvider<RoomResponse> {
  RoomProvider() : super("Room");

  @override
  RoomResponse fromJson(data) {
    return RoomResponse.fromJson(data);
  }

  Future<String> save(int userId, int roomId) async {
    var url = Uri.parse("$fullUrl/$userId/save/$roomId");

    Map<String, String> headers = createHeaders();

    var response = await http!.put(url, headers: headers);

    if (isValidResponseCode(response)) {
      var data = response.body;
      return data;
    } else {
      throw Exception("An error occured!");
    }
  }

  Future<String> removeSaved(int userId, int roomId) async {
    var url = Uri.parse("$fullUrl/$userId/remove-save/$roomId");

    Map<String, String> headers = createHeaders();

    var response = await http!.delete(url, headers: headers);

    if (isValidResponseCode(response)) {
      var data = response.body;
      return data;
    } else {
      throw Exception("An error occured!");
    }
  }
}
