import 'dart:convert';
import 'dart:io';

import 'package:flutter/cupertino.dart';
import 'package:http/io_client.dart';
import 'package:the_lions_den_mobile/model/facility/facility_response.dart';
import 'package:the_lions_den_mobile/model/reservation/reservation_response.dart';
import 'package:the_lions_den_mobile/model/room/room_response.dart';
import 'package:the_lions_den_mobile/providers/base_provider.dart';

class FacilityProvider extends BaseProvider<FacilityResponse> {
  FacilityProvider() : super("Facility");

  @override
  FacilityResponse fromJson(data) {
    return FacilityResponse.fromJson(data);
  }

  Future<List<FacilityResponse>> reccomend(int id) async {
    var url = Uri.parse("$fullUrl/reccommend/$id");

    Map<String, String> headers = createHeaders();

    var response = await http!.get(url, headers: headers);

    if (isValidResponseCode(response)) {
      var data = jsonDecode(response.body);
      return data.map((x) => fromJson(x)).cast<FacilityResponse>().toList();
    } else {
      throw Exception("An error occurred!");
    }
  }
}
