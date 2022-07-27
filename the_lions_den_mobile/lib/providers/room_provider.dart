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
}
