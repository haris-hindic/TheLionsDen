import 'dart:convert';
import 'dart:io';

import 'package:flutter/cupertino.dart';
import 'package:http/io_client.dart';
import 'package:the_lions_den_mobile/model/room_type/room_type_response.dart';
import 'package:the_lions_den_mobile/providers/base_provider.dart';

class RoomTypeProvider extends BaseProvider<RoomTypeResponse> {
  RoomTypeProvider() : super("RoomType");

  @override
  RoomTypeResponse fromJson(data) {
    return RoomTypeResponse.fromJson(data);
  }
}
