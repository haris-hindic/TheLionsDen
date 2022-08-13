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
  ..coverImage = json['coverImage'] as String?
  ..roomType = json['roomType'] == null
      ? null
      : RoomTypeResponse.fromJson(json['roomType'] as Map<String, dynamic>)
  ..roomAmenities = (json['roomAmenities'] as List<dynamic>?)
      ?.map((e) => RoomAmenityResponse.fromJson(e as Map<String, dynamic>))
      .toList();

Map<String, dynamic> _$RoomResponseToJson(RoomResponse instance) =>
    <String, dynamic>{
      'roomId': instance.roomId,
      'price': instance.price,
      'name': instance.name,
      'amenities': instance.amenities,
      'roomTypeName': instance.roomTypeName,
      'coverImage': instance.coverImage,
      'roomType': instance.roomType,
      'roomAmenities': instance.roomAmenities,
    };
