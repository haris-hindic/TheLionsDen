import 'dart:convert';
import 'dart:io';

import 'package:flutter/cupertino.dart';
import 'package:http/io_client.dart';
import 'package:the_lions_den_mobile/model/reservation/reservation_response.dart';
import 'package:the_lions_den_mobile/model/room/room_response.dart';
import 'package:the_lions_den_mobile/providers/base_provider.dart';

class ReservationProvider extends BaseProvider<ReservationResponse> {
  ReservationProvider() : super("Reservation");

  @override
  ReservationResponse fromJson(data) {
    return ReservationResponse.fromJson(data);
  }

  Future<String> cancel(int id) async {
    var url = Uri.parse("$fullUrl/$id/cancel");

    Map<String, String> headers = createHeaders();

    var response = await http!.put(url, headers: headers);

    if (isValidResponseCode(response)) {
      var data = response.body;
      return data;
    } else {
      throw Exception("An error occured!");
    }
  }
}
