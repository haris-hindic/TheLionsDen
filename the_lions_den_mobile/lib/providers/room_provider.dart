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

  Future<bool> checkAvailabilty(
      int roomId, DateTime arrival, DateTime deparutre) async {
    var url = "$fullUrl/$roomId/check-availability";

    String queryString =
        getQueryString({'arrival': arrival, 'departure': deparutre});
    url = url + "?" + queryString;

    var uri = Uri.parse(url);

    Map<String, String> headers = createHeaders();

    var response = await http!.get(uri, headers: headers);

    if (isValidResponseCode(response)) {
      var data = response.body;
      return data.toLowerCase() == 'true';
    } else {
      throw Exception("An error occured!");
    }
  }

  Future<List<dynamic>> getBookedDates(int roomId) async {
    var url = Uri.parse("$fullUrl/$roomId/booked-dates");

    Map<String, String> headers = createHeaders();

    var response = await http!.get(url, headers: headers);

    if (isValidResponseCode(response)) {
      var a = jsonDecode(response.body);
      return a;
    } else {
      throw Exception("An error occured!");
    }
  }
}
