// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'room_response.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

RoomResponse _$RoomResponseFromJson(Map<String, dynamic> json) => RoomResponse()
  ..roomId = json['roomId'] as int?
  ..price = (json['price'] as num?)?.toDouble()
  ..name = json['name'] as String?
  ..amenities = json['amenities'] as String?
  ..roomTypeName = json['roomTypeName'] as String?
  ..coverImage = json['coverImage'] as String?;

Map<String, dynamic> _$RoomResponseToJson(RoomResponse instance) =>
    <String, dynamic>{
      'roomId': instance.roomId,
      'price': instance.price,
      'name': instance.name,
      'amenities': instance.amenities,
      'roomTypeName': instance.roomTypeName,
      'coverImage': instance.coverImage,
    };
