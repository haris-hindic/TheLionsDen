// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'room_image_response.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

RoomImageResponse _$RoomImageResponseFromJson(Map<String, dynamic> json) =>
    RoomImageResponse()
      ..roomImageId = json['roomImageId'] as int?
      ..roomTypeId = json['roomTypeId'] as int?
      ..image = json['image'] as String?;

Map<String, dynamic> _$RoomImageResponseToJson(RoomImageResponse instance) =>
    <String, dynamic>{
      'roomImageId': instance.roomImageId,
      'roomTypeId': instance.roomTypeId,
      'image': instance.image,
    };
